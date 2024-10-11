using Ecs;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class AttackTargetSystem : IFixedUpdateSystem
{
    private readonly ComponentPool<CombatComponent> _combatPool;
    private readonly ComponentPool<TargetIDComponent> _targetIDPool;
    private readonly ComponentPool<TransformComponent> _transformPool;
    private readonly ComponentPool<MoveToPositionData> _moveToPositionPool;

    private readonly ComponentPool<AttackCooldownComponent> _attackCooldownPool;
    private readonly ComponentPool<AttackDurationComponent> _attackDurationPool;
    private readonly ComponentPool<AttackComponent> _attackComponent;
    private readonly ComponentPool<RotationComponent> _rotationPool;

    public void OnFixedUpdate(int entityIndex)
    {
        if (!_targetIDPool.HasComponent(entityIndex))
        {
            return;
        }

        ref var targetID = ref _targetIDPool.GetComponent(entityIndex).TargetID;

        var myPosition = _transformPool.GetComponent(entityIndex).Value.position;
        var targetPosition = _transformPool.GetComponent(targetID).Value.position;
        ref var combat = ref _combatPool.GetComponent(entityIndex);

        if (Vector3.Distance(myPosition, targetPosition) <= combat.AttackDistance)
        {
            _moveToPositionPool.RemoveComponent(entityIndex);

            SetRotation(entityIndex, targetPosition, myPosition);

            if (_attackCooldownPool.HasComponent(entityIndex) || _attackDurationPool.HasComponent(entityIndex))
            {
                return;
            }

            _attackDurationPool.SetComponent(entityIndex, new AttackDurationComponent()
            {
                Value = combat.AnimationTime
            });
        }
        else
        {
            _attackComponent.RemoveComponent(entityIndex);
            _moveToPositionPool.SetComponent(entityIndex, new MoveToPositionData
            {
                Destination = targetPosition
            });
        }
    }

    private void SetRotation(int entityIndex, Vector3 targetPosition, Vector3 myPosition)
    {
        Vector3 direction = targetPosition - myPosition;

        _rotationPool.SetComponent(entityIndex, new RotationComponent()
        {
            Direction = direction.normalized
        });
    }
}