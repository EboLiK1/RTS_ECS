using Ecs;
using UnityEngine;

public sealed class RotationSystem : IFixedUpdateSystem
{
    private readonly ComponentPool<RotationComponent> _rotationPool;
    private readonly ComponentPool<TransformComponent> _transformPool;
    private float RotationThreshold = 1f;

    public void OnFixedUpdate(int entityIndex)
    {
        if (!_rotationPool.HasComponent(entityIndex))
        {
            return;
        }

        var fixedDeltaTime = Time.fixedDeltaTime;

        ref var direction = ref _rotationPool.GetComponent(entityIndex).Direction;
        ref var transform = ref _transformPool.GetComponent(entityIndex).Value;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 360f * fixedDeltaTime);

        if (Quaternion.Angle(transform.rotation, targetRotation) < RotationThreshold)
        {
            // Действия по завершению поворота
        }
    }
}