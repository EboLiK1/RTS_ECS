using Ecs;
using UnityEngine;

public sealed class CameraEntity : Entity
{
    protected override void Init()
    {
        SetData(new MoveSpeedComponent
        {
            Value = 5f
        });

        SetData(new TransformComponent()
        {
            Value = GetComponent<Transform>()
        });

        SetData(new CameraComponent()
        {
            Camera = GetComponent<Camera>()
        });
    }
}