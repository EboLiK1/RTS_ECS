using Ecs;

public sealed class IdleStateMachine : IFixedUpdateSystem
{
    private ComponentPool<CommandRequest> _commandPool;

    private readonly IIdleState[] _states;

    private bool _isEntered;

    public IdleStateMachine(params IIdleState[] states)
    {
        _states = states;

        foreach (var state in states)
        {
            EcsModule.World.Inject(state);
        }
    }

    void IFixedUpdateSystem.OnFixedUpdate(int entity)
    {
        if (_commandPool.HasComponent(entity))
        {
            Exit(entity);
            return;
        }

        Enter(entity);
        Update(entity);
    }

    private void Enter(int entity)
    {
        if (_isEntered)
        {
            return;
        }

        for (int i = 0, count = _states.Length; i < count; i++)
        {
            var state = _states[i];
            state.OnEnter(entity);
        }

        _isEntered = true;
    }

    private void Exit(int entity)
    {
        if (!_isEntered)
        {
            return;
        }

        for (int i = 0, count = _states.Length; i < count; i++)
        {
            var state = _states[i];
            state.OnExit(entity);
        }

        _isEntered = false;
    }

    private void Update(int entity)
    {
        for (int i = 0, count = _states.Length; i < count; i++)
        {
            var state = _states[i];
            state.OnUpdate(entity);
        }
    }
}