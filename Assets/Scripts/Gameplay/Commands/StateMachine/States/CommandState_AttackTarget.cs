using Ecs;
using UnityEngine;

public sealed class CommandState_AttackTarget : CommandState
{
    private ComponentPool<TargetIDComponent> _targetIDPool;
    private ComponentPool<AttackComponent> _attackPool;
    private ComponentPool<AttackDurationComponent> _attackDurationPool;
    private ComponentPool<AttackCooldownComponent> _attackCooldownPool;

    private ComponentPool<MoveToPositionData> _moveToPositionPool;
    //private ComponentPool<HitPointsComponent> _hitPointsPool;

    private EcsWorld _world;

    public override bool MatchesType(CommandType type)
    {
        return type is CommandType.ATTACK_TARGET;
    }

    public override void Enter(int entity, object args)
    {
        Debug.Log("ATTACK_ENTER");
        _targetIDPool.SetComponent(entity, new TargetIDComponent
        {
            TargetID = ((Entity)args).Id
        });
    }

    public override void Exit(int entity)
    {
        _targetIDPool.RemoveComponent(entity);
        _attackPool.RemoveComponent(entity);
        _attackDurationPool.RemoveComponent(entity);
        _attackCooldownPool.RemoveComponent(entity);
        _moveToPositionPool.RemoveComponent(entity);
    }

    public override void Update(int entity)
    {
        //if (!IsTargetExists(entity))
        //{
        //    Complete(entity);
        //}
    }

    //private bool IsTargetExists(int entity)
    //{
    //    ref var targetId = ref _attackPool.GetComponent(entity).TargetId;
    //    if (!_world.IsEntityExists(targetId))
    //    {
    //        return false;
    //    }

    //    ref var targetHitPoints = ref _hitPointsPool.GetComponent(targetId);
    //    return targetHitPoints.Current > 0;
    //}
}