using Ecs;

public sealed class CommandStateMachine : IFixedUpdateSystem
{
    private readonly CommandState[] _states;

    private ComponentPool<CommandRequest> _commandPool;

    private bool _isEntered;
    private CommandRequest _command;

    public CommandStateMachine(params CommandState[] states)
    {
        _states = states;

        foreach (var state in states)
        {
            EcsModule.World.Inject(state);
        }
    }

    public void OnFixedUpdate(int entityIndex)
    {
        if (!_commandPool.HasComponent(entityIndex))
        {
            Exit(entityIndex);
            return;
        }

        ref var command = ref _commandPool.GetComponent(entityIndex);
        if (!command.Equals(_command))
        {
            Exit(entityIndex);
            _command = command;
        }

        if (_command.Status is CommandStatus.COMPLETE or CommandStatus.FAIL)
        {
            Exit(entityIndex);
            return;
        }

        _command.Status = CommandStatus.PLAYING;

        Enter(entityIndex);
        Update(entityIndex);
    }


    private void Enter(int entityIndex)
    {
        if (_isEntered)
        {
            return;
        }

        for (int i = 0, count = _states.Length; i < count; i++)
        {
            var state = _states[i];
            if (state.MatchesType(_command.Type))
            {
                state.Enter(entityIndex, _command.Args);
            }
        }

        _isEntered = true;
    }

    private void Exit(int entityIndex)
    {
        if (!_isEntered)
        {
            return;
        }

        for (int i = 0, count = _states.Length; i < count; i++)
        {
            var state = _states[i];
            if (state.MatchesType(_command.Type))
            {
                state.Exit(entityIndex);
            }
        }

        _isEntered = false;
        _command = default;
    }

    private void Update(int entityIndex)
    {
        for (int i = 0, count = _states.Length; i < count; i++)
        {
            var state = _states[i];
            if (state.MatchesType(_command.Type))
            {
                state.Update(entityIndex);
            }
        }
    }
}