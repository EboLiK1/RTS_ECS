using Ecs;
using UnityEngine;

public sealed class MoveStepSystem : IFixedUpdateSystem
{
    private readonly ComponentPool<MoveStepData> _stepDataPool;
    private readonly ComponentPool<MoveSpeedComponent> _speedPool;
    private readonly ComponentPool<TransformComponent> _transformPool;

    public void OnFixedUpdate(int entityIndex)
    {
        if (!_stepDataPool.HasComponent(entityIndex))
        {
            return;
        }

        ref var stepData = ref _stepDataPool.GetComponent(entityIndex);
        if (stepData.Completed)
        {
            _stepDataPool.RemoveComponent(entityIndex);
            return;
        }

        UpdatePosition(entityIndex, stepData.Direction);

        stepData.Completed = true;
    }

    private void UpdatePosition(int entityIndex, Vector3 direction)
    {
        ref var transform = ref _transformPool.GetComponent(entityIndex).Value;
        ref var moveSpeed = ref _speedPool.GetComponent(entityIndex).Value;

        var moveStep = direction * moveSpeed * Time.fixedDeltaTime;
        transform.position += moveStep;
    }
}