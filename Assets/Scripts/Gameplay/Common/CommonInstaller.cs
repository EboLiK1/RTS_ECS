using Ecs;
using UnityEngine;

[CreateAssetMenu(fileName = "New Installer «Common»", menuName = "Game/GameEngine/Ecs/New Installer «Common»")]
public sealed class CommonInstaller : EcsInstaller
{
    public override void Install(EcsWorld world)
    {
        world.BindComponent<Rigidbody2DComponent>();
        world.BindComponent<TransformComponent>();
        world.BindComponent<RotationComponent>();

        world.BindSystem<RotationSystem>();
    }
}