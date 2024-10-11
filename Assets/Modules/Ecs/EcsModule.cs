using Ecs;
using System;
using UnityEngine;

[DefaultExecutionOrder(-10000)]
public sealed class EcsModule : MonoBehaviour
{
    public static EcsModule Instance { get; private set; }

    public static event Action OnUpdate;
    public static event Action OnFixedUpdate;
    public static event Action OnLateUpdate;

    [SerializeField] private EcsInstaller[] _installers;

    private static EcsWorld _world;
    public static EcsWorld World => _world;

    private SelectedUnitsStack _selectedUnitsStack;
    public SelectedUnitsStack SelectedUnitsStack => _selectedUnitsStack;

    private void Awake()
    {
        Instance = this;

        _world = new EcsWorld();
        _selectedUnitsStack = new SelectedUnitsStack();

        foreach (var installer in _installers)
        {
            installer.Install(_world);
        }

        Debug.Log("¬—≈ »Õ—“¿ÀÀ≈–€ ”—“¿ÕŒ¬À≈Õ€");

        _world.ResolveDependencies();
    }

    private void Update()
    {
        _world.Update();
        OnUpdate?.Invoke();
    }

    private void FixedUpdate()
    {
        _world.FixedUpdate();
        OnFixedUpdate?.Invoke();
    }

    private void LateUpdate()
    {
        _world.LateUpdate();
        OnLateUpdate?.Invoke();
    }
}