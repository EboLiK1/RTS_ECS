namespace Ecs
{
    public interface IComponentPool
    {
        bool HasComponent(int entity);

        void RemoveComponent(int entity);

        internal void AllocateComponent();

        internal object GetRawComponent(int entity);

        internal void SetRawComponent(int entity, object data);
    }
}