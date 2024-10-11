using Ecs;
using System;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-2000)]
[RequireComponent(typeof(Entity))]
public abstract class EntityBehaviour : MonoBehaviour
{
    private Entity _entity;

    private readonly List<IUpdateSystem> _entityUpdateSystems = new();
    private readonly List<IFixedUpdateSystem> _entityFixedUpdateSystems = new();
    private readonly List<ILateUpdateSystem> _entityLateUpdateSystems = new();

    private readonly List<(Type, IEcsObserver)> _observers = new();

    protected abstract IEnumerable<ISystem> ProvideSystems();
    //protected abstract IEnumerable<(Type, IEcsObserver)> ProvideObservers();

    private void Awake()
    {
        _entity = GetComponent<Entity>();
        var systems = ProvideSystems();
        RegisterSystems(systems);

        //var observers = ProvideObservers();
        //_observers.AddRange(observers);
    }

    private void OnEnable()
    {
        EcsModule.OnUpdate += OnUpdate;
        EcsModule.OnFixedUpdate += OnFixedUpdate;
        EcsModule.OnLateUpdate += OnLateUpdate;
        SubscribeObservers();
    }

    private void OnDisable()
    {
        EcsModule.OnUpdate -= OnUpdate;
        EcsModule.OnFixedUpdate -= OnFixedUpdate;
        EcsModule.OnLateUpdate -= OnLateUpdate;
        UnsubscribeObservers();
    }

    #region Updates
    private void OnUpdate()
    {
        if (_entity.IsExists())
        {
            foreach (var entityUpdateSystems in _entityUpdateSystems)
            {
                entityUpdateSystems.OnUpdate(_entity.Id);
            }
        }
    }

    private void OnFixedUpdate()
    {
        if (_entity.IsExists())
        {
            foreach (var entityFixedUpdateSystem in _entityFixedUpdateSystems)
            {
                entityFixedUpdateSystem.OnFixedUpdate(_entity.Id);
            }
        }
    }

    private void OnLateUpdate()
    {
        if (_entity.IsExists())
        {
            foreach (var entityLateUpdateSystems in _entityLateUpdateSystems)
            {
                entityLateUpdateSystems.OnLateUpdate(_entity.Id);
            }
        }
    }
    #endregion

    private void RegisterSystems(IEnumerable<object> systems)
    {
        var world = EcsModule.World;
        foreach (var system in systems)
        {
            world.Inject(system);

            if (system is IUpdateSystem update)
            {
                _entityUpdateSystems.Add(update);
            }

            if (system is IFixedUpdateSystem fixedUpdate)
            {
                _entityFixedUpdateSystems.Add(fixedUpdate);
            }

            if (system is ILateUpdateSystem lateUpdate)
            {
                _entityLateUpdateSystems.Add(lateUpdate);
            }
        }
    }

    #region Observers
    private void SubscribeObservers()
    {
        var world = EcsModule.World;
        foreach (var (eventType, observer) in _observers)
        {
            world.Subscribe(_entity.Id, eventType, observer);
        }
    }
    private void UnsubscribeObservers()
    {
        var world = EcsModule.World;
        foreach (var (eventType, observer) in _observers)
        {
            world.Unsubscribe(_entity.Id, eventType, observer);
        }
    }
    #endregion
}