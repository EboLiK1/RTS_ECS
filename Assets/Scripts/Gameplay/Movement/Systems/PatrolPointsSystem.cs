using Ecs;
using UnityEngine;

public sealed class PatrolPointsSystem : IUpdateSystem
{
    private ComponentPool<TransformComponent> _transformPool;
    private ComponentPool<PatrolData> _patrolPool;
    private ComponentPool<MoveToPositionData> _movePool;

    public void OnUpdate(int entityIndex)
    {
        if (!_patrolPool.HasComponent(entityIndex))
        {
            return;
        }

        ref var transform = ref _transformPool.GetComponent(entityIndex).Value;
        ref var patrolData = ref _patrolPool.GetComponent(entityIndex);

        var targetPoint = patrolData.GetCurrentPoint();

        if (Vector3.Distance(transform.position, targetPoint) <= patrolData.StoppingDistance)
        {
            patrolData.MoveNext();
            return;
        }

        _movePool.SetComponent(entityIndex, new MoveToPositionData
        {
            Destination = targetPoint
        });
    }
}