using Ecs;
using UnityEngine;

[CreateAssetMenu(fileName = "New Installer «Camera»", menuName = "Game/GameEngine/Ecs/New Installer «Camera»")]
public sealed class CameraInstaller : EcsInstaller
{
    public override void Install(EcsWorld world)
    {
        world.BindComponent<CameraComponent>();
        world.BindSystem<CameraSystem>();
    }
}