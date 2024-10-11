using Ecs;

public sealed class MoveToPositionSystem : IFixedUpdateSystem
{
    private readonly ComponentPool<TransformComponent> _transformPool;
    private readonly ComponentPool<MoveToPositionData> _moveToPositionPool;
    private readonly ComponentPool<MoveStepData> _moveStepPool;
    private readonly ComponentPool<RotationComponent> _rotationPool;

    public void OnFixedUpdate(int entityIndex)
    {
        if (!_moveToPositionPool.HasComponent(entityIndex))
        {
            return;
        }

        ref var moveData = ref _moveToPositionPool.GetComponent(entityIndex);
        ref var transform = ref _transformPool.GetComponent(entityIndex);

        var currentPosiiton = transform.Value.position;
        var targetPosition = moveData.Destination;
        var distanceVector = targetPosition - currentPosiiton;

        moveData.IsReached = distanceVector.sqrMagnitude <= moveData.StoppingDistance;
        if (moveData.IsReached)
        {
            return;
        }

        _rotationPool.SetComponent(entityIndex, new RotationComponent
        {
            Direction = distanceVector.normalized
        });

        _moveStepPool.SetComponent(entityIndex, new MoveStepData
        {
            Direction = distanceVector.normalized
        });
    }
}