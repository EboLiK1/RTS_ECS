using System;

namespace Ecs
{
    public class ComponentPool<T> : IComponentPool where T : struct
    {
        private struct Component
        {
            public bool Exists;
            public T Value;
        }

        private Component[] components = new Component[256];

        private int size = 0;

        public ref T GetComponent(int entity)
        {
            ref var component = ref components[entity];
            return ref component.Value;
        }

        public void SetComponent(int entity, T data)
        {
            ref var component = ref components[entity];
            component.Exists = true;
            component.Value = data;
        }

        public void SetComponent(int entity, ref T data)
        {
            ref var component = ref components[entity];
            component.Exists = true;
            component.Value = data;
        }

        public void RemoveComponent(int entity)
        {
            ref var component = ref components[entity];
            component.Exists = false;
        }

        public bool HasComponent(int entity)
        {
            return components[entity].Exists;
        }

        void IComponentPool.AllocateComponent()
        {
            if (size + 1 >= components.Length)
            {
                Array.Resize(ref components, components.Length * 2);
            }

            components[size] = new Component
            {
                Exists = false,
                Value = default
            };

            size++;
        }

        object IComponentPool.GetRawComponent(int entity)
        {
            return components[entity].Value;
        }

        void IComponentPool.SetRawComponent(int entity, object data)
        {
            components[entity] = new Component
            {
                Exists = true,
                Value = (T)data
            };
        }
    }
}
