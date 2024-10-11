using Ecs;
using UnityEngine;

public class AnimatorMachine : MonoBehaviour
{
    private static readonly int STATE = Animator.StringToHash("State");

    private Entity _entity;
    private Animator _animator;
    private int _stateId;

    private readonly ComponentPool<AttackComponent> _attackPool;
    private readonly ComponentPool<AttackDurationComponent> _attackDurationPool;

    protected virtual void Awake()
    {
        _entity = GetComponent<Entity>();
        _animator = GetComponent<Animator>();
        _stateId = _animator.GetInteger(STATE);
    }

    public void ChangeState(int stateId)
    {
        if (_stateId == stateId)
        {
            return;
        }

        _stateId = stateId;
        _animator.SetInteger(STATE, _stateId);
    }

    public void FinishAttack()
    {
        _attackPool.RemoveComponent(_entity.Id);
    }
}