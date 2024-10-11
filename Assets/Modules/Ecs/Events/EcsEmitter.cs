using System;
using System.Collections.Generic;

namespace Ecs
{
    public sealed class EcsEmitter<T> : IEcsEmitter where T : struct
    {
        private readonly List<IEcsObserver<T>> _observers = new();
        private readonly Dictionary<int, Listener> _entityListeners = new();

        public void SendEvent(int entity, T @event)
        {
            for (int i = 0, count = this._observers.Count; i < count; i++)
            {
                var observer = this._observers[i];
                observer.Handle(entity, @event);
            }
            
            if (this._entityListeners.TryGetValue(entity, out var listener))
            {
                listener.Invoke(entity, @event);
            }
        }

        internal void AddObserver(IEcsObserver<T> observer)
        {
            this._observers.Add(observer);
        }

        IEnumerable<object> IEcsEmitter.GetObservers()
        {
            return this._observers;
        }

        void IEcsEmitter.Subscribe(int entity, IEcsObserver observer)
        {
            if (observer is not IEcsObserver<T> tObserver)
            {
                return;
            }

            if (!this._entityListeners.TryGetValue(entity, out var listener))
            {
                listener = new Listener();
                this._entityListeners.Add(entity, listener);
            }

            listener.observers.Add(tObserver);
        }

        void IEcsEmitter.Unsubscribe(int entity, IEcsObserver observer)
        {
            if (observer is not IEcsObserver<T> tObserver)
            {
                return;
            }
            
            if (this._entityListeners.TryGetValue(entity, out var listener))
            {
                listener.observers.Remove(tObserver);
            }
        }

        internal void Subscribe(int entity, Action<T> callback)
        {
            if (!this._entityListeners.TryGetValue(entity, out var listener))
            {
                listener = new Listener();
                this._entityListeners.Add(entity, listener);
            }
            
            listener.onEvent += callback;
        }
        
        internal void Unsubscribe(int entity, Action<T> callback)
        {
            if (this._entityListeners.TryGetValue(entity, out var listener))
            {
                listener.onEvent -= callback;
            }
        }
        
        private sealed class Listener
        {
            internal event Action<T> onEvent;
            
            internal readonly List<IEcsObserver<T>> observers = new();
            private readonly List<IEcsObserver<T>> cache = new();

            public void Invoke(int entity, T @event)
            {
                this.onEvent?.Invoke(@event);
                
                this.cache.Clear();
                this.cache.AddRange(this.observers);

                for (int i = 0, count = this.cache.Count; i < count; i++)
                {
                    var observer = this.cache[i];
                    observer.Handle(entity, @event);
                }
            }
        }
    }
}