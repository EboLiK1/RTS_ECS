using Ecs;
using UnityEngine;

public sealed class CommandState_MoveToPosition : CommandState
{
    private readonly ComponentPool<MoveToPositionData> _moveToPositionPool;
    private readonly ComponentPool<AnimatorComponent> _animatorPool;
    private readonly ComponentPool<RotationComponent> _rotationPool;

    public override bool MatchesType(CommandType type)
    {
        return type is CommandType.MOVE_TO_POSITION;
    }

    public override void Enter(int entityIndex, object args)
    {
        _moveToPositionPool.SetComponent(entityIndex, new MoveToPositionData
        {
            Destination = (Vector3)args,
            StoppingDistance = 0.3f
        });
    }

    public override void Update(int entityIndex)
    {
        ref var moveData = ref _moveToPositionPool.GetComponent(entityIndex);
        if (moveData.IsReached)
        {
            Complete(entityIndex);
        }
    }

    public override void Exit(int entityIndex)
    {
        _moveToPositionPool.RemoveComponent(entityIndex);
        _rotationPool.RemoveComponent(entityIndex);
    }
}