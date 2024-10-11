using Ecs;
using UnityEngine;

[CreateAssetMenu(fileName = "New Installer «Selection»", menuName = "Game/GameEngine/Ecs/New Installer «Selection»")]
public sealed class SelectionInstaller : EcsInstaller
{
    public override void Install(EcsWorld world)
    {
        world.BindComponent<SelectionDataComponent>();
    }
}
