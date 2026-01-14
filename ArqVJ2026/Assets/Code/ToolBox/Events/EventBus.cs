using ianco99.ToolBox.Pool;
using ianco99.ToolBox.Services;
using System;
using System.Collections.Generic;

namespace ianco99.ToolBox.Events
{
    public sealed class EventBus : IService
    {
        private readonly Dictionary<Type, List<Delegate>> subscribers = new Dictionary<Type, List<Delegate>>();

        private readonly ConcurrentPool eventPool = new ConcurrentPool();
        public bool IsPersistance => false;

        public void Subscribe<EventType>(Action<EventType> callback) where EventType : struct, IEvent
        {
            Type eventType = typeof(EventType);

            if(!subscribers.ContainsKey(eventType))
            {
                subscribers.Add(eventType, new List<Delegate>());
            }
            
            subscribers[eventType].Add(callback);
        }

        public void UnSubscribe<EventType>(Action<EventType> callback) where EventType : struct, IEvent
        {
            Type eventType = typeof(EventType);

            if(!subscribers.TryGetValue(eventType, out List<Delegate> subscriptions))
            {
                subscriptions.Remove(callback);
            }
        }

        public void Raise<EventType>(params object[] parameters) where EventType : struct, IEvent
        {
            Type eventType = typeof(EventType);
            EventType raisingEvent = eventPool.Get<EventType>(parameters);
            if(subscribers.TryGetValue(eventType, out List<Delegate> subscriptions))
            {
                foreach (Delegate callback in subscriptions)
                {
                    ((Action<EventType>)callback)?.Invoke(raisingEvent);
                }
            }
            eventPool.Release(raisingEvent);
        }

        public void Clear()
        {
            subscribers.Clear();
        }
    }
}
