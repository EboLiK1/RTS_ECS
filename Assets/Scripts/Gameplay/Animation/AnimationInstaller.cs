using Ecs;
using UnityEngine;

[CreateAssetMenu(fileName = "New Installer «Animation»", menuName = "Game/GameEngine/Ecs/New Installer «Animation»")]
public sealed class AnimationInstaller : EcsInstaller
{
    public override void Install(EcsWorld world)
    {
        world.BindComponent<AnimatorComponent>();
    }
}