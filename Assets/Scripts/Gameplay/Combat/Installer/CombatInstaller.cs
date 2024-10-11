using Ecs;
using UnityEngine;

[CreateAssetMenu(fileName = "New Installer «Combat»", menuName = "Game/GameEngine/Ecs/New Installer «Combat»")]
public sealed class CombatInstaller : EcsInstaller
{
    public override void Install(EcsWorld world)
    {
        world.BindComponent<CombatComponent>();
        world.BindComponent<TargetIDComponent>();
        world.BindComponent<AttackDurationComponent>();
        world.BindComponent<AttackCooldownComponent>();
        world.BindComponent<AttackComponent>();

        world.BindSystem<AttackTargetSystem>();
        world.BindSystem<AttackCooldownSystem>();
        world.BindSystem<AttackDurationSystem>();
    }
}