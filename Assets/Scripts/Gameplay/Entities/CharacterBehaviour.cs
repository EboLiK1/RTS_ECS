using Ecs;
using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Entity))]
public sealed class CharacterBehaviour : EntityBehaviour
{
    protected override IEnumerable<ISystem> ProvideSystems()
    {
        yield return new IdleStateMachine();

        yield return new CommandStateMachine(new CommandState_MoveToPosition(),
                                             new CommandState_PatrolByPoints(),
                                             new CommandState_AttackTarget());

        yield return new CharacterAnimatorSystem();
    }

    //protected override IEnumerable<(Type, IEcsObserver)> ProvideObservers()
    //{
    //    throw new NotImplementedException();
    //}
}