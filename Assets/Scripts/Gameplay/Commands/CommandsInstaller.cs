using Ecs;
using UnityEngine;

[CreateAssetMenu(fileName = "New Installer «Commands»", menuName = "Game/GameEngine/Ecs/New Installer «Commands»")]
public sealed class CommandsInstaller : EcsInstaller
{
    public override void Install(EcsWorld world)
    {
        world.BindComponent<CommandRequest>();
    }
}