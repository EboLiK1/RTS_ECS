public interface IIdleState
{
    void OnEnter(int entityIndex);
    void OnUpdate(int entityIndex);
    void OnExit(int entityIndex);
}