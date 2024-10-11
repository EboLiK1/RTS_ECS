using Ecs;
using UnityEngine;

[CreateAssetMenu(fileName = "New Installer «Move»", menuName = "Game/GameEngine/Ecs/New Installer «Move»")]
public sealed class MoveInstaller : EcsInstaller
{
    public override void Install(EcsWorld world)
    {
        world.BindComponent<MoveSpeedComponent>();
        world.BindComponent<MoveStepData>();
        world.BindComponent<MoveToPositionData>();
        world.BindComponent<PatrolData>();

        world.BindSystem<MoveStepSystem>();
        world.BindSystem<MoveToPositionSystem>();
        world.BindSystem<PatrolPointsSystem>();
    }
}