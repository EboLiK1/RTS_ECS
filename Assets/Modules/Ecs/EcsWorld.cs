using System;
using System.Collections.Generic;
using UnityEngine;

namespace Ecs
{
    public sealed class EcsWorld
    {
        private readonly Dictionary<Type, IComponentPool> _componentPools = new();
        private readonly Dictionary<Type, IEcsEmitter> _eventEmitters = new();

        private readonly List<ISystem> _allSystems = new();
        private readonly List<IUpdateSystem> _updateSystems = new();
        private readonly List<IFixedUpdateSystem> _fixedUpdateSystems = new();
        private readonly List<ILateUpdateSystem> _lateUpdateSystems = new();

        private readonly List<bool> _entities = new();
        private readonly List<int> _cache = new();

        private readonly List<object> _externalServices = new();

        #region Entities

        public int CreateEntity()
        {
            var id = 0;
            var count = _entities.Count;

            for (; id < count; id++)
            {
                if (!_entities[id])
                {
                    _entities[id] = true;
                    return id;
                }
            }

            id = count;
            _entities.Add(true);

            foreach (var pool in _componentPools.Values)
            {
                pool.AllocateComponent();
            }

            return id;
        }

        public bool IsEntityExists(int entityIndex)
        {
            if (entityIndex < 0 || entityIndex >= _entities.Count)
            {
                return false;
            }

            return _entities[entityIndex];
        }

        public void DestroyEntity(int entity)
        {
            _entities[entity] = false;

            foreach (var componentPool in _componentPools.Values)
            {
                componentPool.RemoveComponent(entity);
            }
        }

        #endregion

        #region Components

        public ref T GetComponent<T>(int entity) where T : struct
        {
            CheckComponentPool<T>();

            var tComponentPool = (ComponentPool<T>)_componentPools[typeof(T)];
            return ref tComponentPool.GetComponent(entity);
        }

        public void SetComponent<T>(int entity, ref T component) where T : struct
        {
            CheckComponentPool<T>();

            var tComponentPool = (ComponentPool<T>)_componentPools[typeof(T)];
            tComponentPool.SetComponent(entity, ref component);
        }

        private void CheckComponentPool<T>() where T : struct
        {
            if (!_componentPools.TryGetValue(typeof(T), out var componentPool))
            {
                throw new Exception($"Component pool of type {typeof(T).Name} is not found!");
            }
        }

        #endregion

        #region Events

        public void SendEvent<T>(int entity, T @event) where T : struct
        {
            if (_eventEmitters.TryGetValue(typeof(T), out var emitter))
            {
                var tEmitter = (EcsEmitter<T>)emitter;
                tEmitter.SendEvent(entity, @event);
            }
        }

        public EcsEmitter<T> GetEmitter<T>() where T : struct
        {
            if (!_eventEmitters.TryGetValue(typeof(T), out var componentPool))
            {
                throw new Exception($"Component pool of type {typeof(T).Name} is not found!");
            }

            return (EcsEmitter<T>)componentPool;
        }

        #endregion

        #region Updates

        public void Update()
        {
            _cache.Clear();
            for (int id = 0, count = _entities.Count; id < count; id++)
            {
                if (_entities[id])
                {
                    _cache.Add(id);
                }
            }

            var entityCount = _cache.Count;

            for (int i = 0, count = _updateSystems.Count; i < count; i++)
            {
                var system = _updateSystems[i];

                for (var e = 0; e < entityCount; e++)
                {
                    var id = _cache[e];
                    system.OnUpdate(id);
                }
            }
        }

        public void FixedUpdate()
        {
            _cache.Clear();
            for (int id = 0, count = _entities.Count; id < count; id++)
            {
                if (_entities[id])
                {
                    _cache.Add(id);
                }
            }

            var entityCount = _cache.Count;

            for (int i = 0, count = _fixedUpdateSystems.Count; i < count; i++)
            {
                var system = _fixedUpdateSystems[i];

                for (var e = 0; e < entityCount; e++)
                {
                    var id = _cache[e];
                    system.OnFixedUpdate(id);
                }
            }
        }

        public void LateUpdate()
        {
            _cache.Clear();
            for (int id = 0, count = _entities.Count; id < count; id++)
            {
                if (_entities[id])
                {
                    _cache.Add(id);
                }
            }

            var entityCount = _cache.Count;

            for (int i = 0, count = _lateUpdateSystems.Count; i < count; i++)
            {
                var system = _lateUpdateSystems[i];

                for (var e = 0; e < entityCount; e++)
                {
                    var id = _cache[e];
                    system.OnLateUpdate(id);
                }
            }
        }

        #endregion

        #region Bind

        public void BindComponent<T>() where T : struct
        {
            var pool = new ComponentPool<T>();
            _componentPools.Add(typeof(T), pool);
        }

        public void BindSystem<T>() where T : ISystem, new()
        {
            var system = new T();
            _allSystems.Add(system);

            AddSystem(system);
        }

        public void BindSystem(ISystem system)
        {
            _allSystems.Add(system);

            AddSystem(system);
        }

        private void AddSystem(ISystem system)
        {
            if (system is IUpdateSystem updateSystem)
            {
                _updateSystems.Add(updateSystem);
            }

            if (system is IFixedUpdateSystem fixedUpdateSystem)
            {
                _fixedUpdateSystems.Add(fixedUpdateSystem);
            }

            if (system is ILateUpdateSystem lateUpdateSystem)
            {
                _lateUpdateSystems.Add(lateUpdateSystem);
            }
        }

        public void BindObserver<E, T>() where T : IEcsObserver<E>, new() where E : struct
        {
            var eventType = typeof(E);
            EcsEmitter<E> tEmitter;

            if (_eventEmitters.TryGetValue(eventType, out var emitter))
            {
                tEmitter = (EcsEmitter<E>)emitter;
            }
            else
            {
                tEmitter = new EcsEmitter<E>();
                _eventEmitters.Add(eventType, tEmitter);
            }

            tEmitter.AddObserver(new T());
        }
        #endregion

        #region Install

        public void ResolveDependencies()
        {
            foreach (var system in _allSystems)
            {
                Inject(system);
            }

            Debug.Log("¬—≈ —»—“≈Ã€ ”—“¿ÕŒ¬À≈Õ€");

            foreach (var eventPool in _eventEmitters.Values)
            {
                foreach (var handler in eventPool.GetObservers())
                {
                    Inject(handler);
                }
            }

            foreach (var system in _allSystems)
            {
                Debug.Log(system.GetType().Name); 
            }
        }

        public void Inject(object target)
        {
            var type = target.GetType();

            var fields = ReflectionUtils.RetrieveFields(type);
            var fieldLength = fields.Count;
            for (var i = 0; i < fieldLength; i++)
            {
                var field = fields[i];
                var fieldType = field.FieldType;
                if (field.GetValue(target) == null)
                {
                    var dependency = ResolveDependency(fieldType);
                    field.SetValue(target, dependency);
                }
            }
        }

        private object ResolveDependency(Type type)
        {
            if (typeof(EcsWorld).IsAssignableFrom(type))
            {
                return this;
            }

            if (typeof(IComponentPool).IsAssignableFrom(type))
            {
                return ResolveComponentPool(type);
            }

            if (typeof(IEcsEmitter).IsAssignableFrom(type))
            {
                return ResolveEventEmitter(type);
            }

            if (ResolveService(type, out var service))
            {
                return service;
            }

            return null;
        }

        private object ResolveComponentPool(Type type)
        {
            var componentType = type.GenericTypeArguments[0];
            if (_componentPools.TryGetValue(componentType, out var pool))
            {
                return pool;
            }

            throw new Exception($"Component pool {componentType.Name} is not found!");
        }

        private object ResolveEventEmitter(Type type)
        {
            var eventType = type.GenericTypeArguments[0];
            if (_eventEmitters.TryGetValue(eventType, out var emitter))
            {
                return emitter;
            }

            throw new Exception($"Event emitter {eventType.Name} is not found!");
        }

        private bool ResolveService(Type type, out object result)
        {
            foreach (var service in _externalServices)
            {
                if (type.IsInstanceOfType(service))
                {
                    result = service;
                    return true;
                }
            }

            result = null;
            return false;
        }

        #endregion

        #region Sub|Unsub
        public void Subscribe<T>(int entity, Action<T> listener) where T : struct
        {
            var eventType = typeof(T);
            if (!_eventEmitters.TryGetValue(eventType, out var emitter))
            {
                emitter = new EcsEmitter<T>();
                _eventEmitters.Add(eventType, emitter);
            }

            var tEmitter = (EcsEmitter<T>)emitter;
            tEmitter.Subscribe(entity, listener);
        }

        public void Subscribe<T>(int entity, IEcsObserver<T> observer) where T : struct
        {
            var eventType = typeof(T);
            if (!_eventEmitters.TryGetValue(eventType, out var emitter))
            {
                emitter = new EcsEmitter<T>();
                _eventEmitters.Add(eventType, emitter);
            }

            emitter.Subscribe(entity, observer);
        }

        public void Subscribe(int entity, Type eventType, IEcsObserver listener)
        {
            Inject(listener);

            if (!_eventEmitters.TryGetValue(eventType, out var emitter))
            {
                var genericEmitter = typeof(EcsEmitter<>).MakeGenericType(eventType);
                emitter = (IEcsEmitter)Activator.CreateInstance(genericEmitter);
                _eventEmitters.Add(eventType, emitter);
            }

            emitter.Subscribe(entity, listener);
        }

        public void Unsubscribe(int entity, Type eventType, IEcsObserver listener)
        {
            if (_eventEmitters.TryGetValue(eventType, out var emitter))
            {
                emitter.Unsubscribe(entity, listener);
            }
        }

        public void Unsubscribe<T>(int entity, Action<T> listener) where T : struct
        {
            if (_eventEmitters.TryGetValue(typeof(T), out var emitter))
            {
                var tEmitter = (EcsEmitter<T>)emitter;
                tEmitter.Unsubscribe(entity, listener);
            }
        }
        #endregion
    }
}