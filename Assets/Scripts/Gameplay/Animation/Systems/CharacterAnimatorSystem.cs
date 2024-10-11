using Ecs;

public sealed class CharacterAnimatorSystem : IUpdateSystem
{
    private ComponentPool<AnimatorComponent> _animatorPool;

    private ComponentPool<MoveStepData> _moveStep;
    private ComponentPool<AttackCooldownComponent> _attackCooldownPool;
    private ComponentPool<AttackDurationComponent> _attackDurationPool;

    public void OnUpdate(int entityIndex)
    {
        ref var animator = ref _animatorPool.GetComponent(entityIndex).Value;
        var animatorState = ResolveState(entityIndex);
        animator.ChangeState(animatorState);
    }

    private int ResolveState(int entityIndex)
    {
        if (_moveStep.HasComponent(entityIndex))
        {
            return AnimatorStateID.RUN;
        }

        if (_attackCooldownPool.HasComponent(entityIndex))
        {
            return AnimatorStateID.AIM;
        }

        if (_attackDurationPool.HasComponent(entityIndex))
        {
            return AnimatorStateID.FIRE;
        }


        return AnimatorStateID.IDLE;
    }
}