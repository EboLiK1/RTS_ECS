using Ecs;
using UnityEngine;

public abstract class EcsInstaller : ScriptableObject
{
    public abstract void Install(EcsWorld world);
}