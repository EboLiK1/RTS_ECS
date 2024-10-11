using System.Collections.Generic;

namespace Ecs
{
    public interface IEcsEmitter
    {
        internal IEnumerable<object> GetObservers();
        
        void Subscribe(int entity, IEcsObserver listener);
        void Unsubscribe(int entity, IEcsObserver listener);
    }
}