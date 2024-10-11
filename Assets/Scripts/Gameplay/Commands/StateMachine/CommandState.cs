using Ecs;

public abstract class CommandState
{
    private ComponentPool<CommandRequest> _commandPool;

    public abstract bool MatchesType(CommandType type);

    public virtual void Enter(int entityIndex, object args) { }

    public virtual void Update(int entityIndex) { }

    public virtual void Exit(int entityIndex) { }

    protected void Fail(int entityIndex)
    {
        if (_commandPool.HasComponent(entityIndex))
        {
            ref var command = ref _commandPool.GetComponent(entityIndex);
            command.Status = CommandStatus.FAIL;
        }
    }

    protected void Complete(int entityIndex)
    {
        if (_commandPool.HasComponent(entityIndex))
        {
            ref var command = ref _commandPool.GetComponent(entityIndex);
            command.Status = CommandStatus.COMPLETE;
        }
    }
}