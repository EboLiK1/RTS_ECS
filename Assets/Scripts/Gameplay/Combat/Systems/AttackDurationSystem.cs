using Ecs;
using UnityEngine;

public sealed class AttackDurationSystem : IFixedUpdateSystem
{
    private readonly ComponentPool<CombatComponent> _combatPool;
    private readonly ComponentPool<AttackDurationComponent> _atttackDurationPool;
    private readonly ComponentPool<AttackCooldownComponent> _attackCooldownPool;
    private readonly ComponentPool<TransformComponent> _transformPool;
    private readonly ComponentPool<TargetIDComponent> _targetIDPool;

    public void OnFixedUpdate(int entityIndex)
    {
        if (!_atttackDurationPool.HasComponent(entityIndex))
        {
            return;
        }

        var deltaTime = Time.fixedDeltaTime;

        ref var duration = ref _atttackDurationPool.GetComponent(entityIndex);
        duration.Value -= deltaTime;

        if (duration.Value <= 0.0f)
        {
            _atttackDurationPool.RemoveComponent(entityIndex);

            ref var combat = ref _combatPool.GetComponent(entityIndex);

            _attackCooldownPool.SetComponent(entityIndex, new AttackCooldownComponent()
            {
                Value = Random.Range(combat.MinTimeBetweenAttack, combat.MaxTimeBetweenAttack)
            });
        }
    }
}