using Ecs;
using UnityEngine;

public sealed class AttackCooldownSystem : IFixedUpdateSystem
{
    private readonly ComponentPool<AttackCooldownComponent> _cooldownPool;
    private readonly ComponentPool<AttackDurationComponent> _atttackDurationPool;

    public void OnFixedUpdate(int entityIndex)
    {
        if (!_cooldownPool.HasComponent(entityIndex))
        {
            return;
        }

        var deltaTime = Time.fixedDeltaTime;

        ref var cooldown = ref _cooldownPool.GetComponent(entityIndex);
        cooldown.Value -= deltaTime;

        if (cooldown.Value <= 0.0f)
        {
            _cooldownPool.RemoveComponent(entityIndex);

            _atttackDurationPool.SetComponent(entityIndex, new AttackDurationComponent()
            {
                Value = 0.5f
            });
        }
    }
}