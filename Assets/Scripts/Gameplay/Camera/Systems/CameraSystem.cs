using Ecs;
using UnityEngine;

public sealed class CameraSystem : IUpdateSystem
{
    private const float SCREEN_BORDER_OFFSET = 5f;

    private readonly ComponentPool<CameraComponent> _cameraPool;
    private readonly ComponentPool<TransformComponent> _transformComponent;
    private readonly ComponentPool<MoveSpeedComponent> _speedComponent;
    private readonly ComponentPool<MoveStepData> _stepData;

    private Vector3 _startMousePosition;
    private bool _isMMBPressed;

    public void OnUpdate(int entityIndex)
    {
        if (!_cameraPool.HasComponent(entityIndex))
        {
            return;
        }

        ref var cameraComponent = ref _cameraPool.GetComponent(entityIndex).Camera;
        ref var transformComponent = ref _transformComponent.GetComponent(entityIndex).Value;
        ref var speedComponent = ref _speedComponent.GetComponent(entityIndex).Value;
        ref var stepComponent = ref _stepData.GetComponent(entityIndex);

        var input = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        var offset = input * speedComponent * Time.deltaTime;
        transformComponent.position += offset;
    }

    private void UpdatePosition(int entityIndex, Vector3 direction)
    {

    }
}