using Ecs;
using System;
using System.Collections.Generic;

public sealed class CameraBehaviour : EntityBehaviour
{
    //protected override IEnumerable<(Type, IEcsObserver)> ProvideObservers()
    //{
    //    throw new NotImplementedException();
    //}

    protected override IEnumerable<ISystem> ProvideSystems()
    {

        //
        //КАК Я ПОНЯЛ НАМ НЕ НАДО БЕХАВИОРЫ ДЛЯ КАЖДОЙ СУЩНОСТИ, НАДО ЛИШЬ ЗАРЕГАТЬ СИСТЕМУ ЧЕРЕЗ ИНСТАЛЕР А В ЕНТИТИ ПОЛОЖИТЬ ДАННЫЕ
        //

        yield return new CameraSystem();

        //yield return new CharacterAnimatorSystem();
        //yield return new CharacterRigidbodySystem();
    }
}