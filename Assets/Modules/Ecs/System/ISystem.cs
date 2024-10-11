namespace Ecs
{
    public interface ISystem
    {
    }

    public interface IUpdateSystem : ISystem
    {
        void OnUpdate(int entityIndex);
    }

    public interface IFixedUpdateSystem : ISystem
    {
        void OnFixedUpdate(int entityIndex);
    }

    public interface ILateUpdateSystem : ISystem
    {
        void OnLateUpdate(int entityIndex);
    }
}