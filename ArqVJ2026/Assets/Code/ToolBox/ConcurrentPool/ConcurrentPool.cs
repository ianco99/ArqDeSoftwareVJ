using System;
using System.Collections.Concurrent;

namespace ianco99.ToolBox.Pool
{
    public class ConcurrentPool
    {
        private readonly ConcurrentDictionary<Type, ConcurrentStack<IResetteable>> concurrentPool = new ConcurrentDictionary<Type, ConcurrentStack<IResetteable>>();

        public ResetteableType Get<ResetteableType>(params object[] parameters) where ResetteableType : IResetteable
        {
            Type reseteableType = typeof(ResetteableType);
            if (!concurrentPool.ContainsKey(reseteableType))
            {
                concurrentPool.TryAdd(reseteableType, new ConcurrentStack<IResetteable>());
            }

            ResetteableType value;
            if (concurrentPool[reseteableType].Count > 0)
            {
                concurrentPool[reseteableType].TryPop(out IResetteable resetteable);
                value = (ResetteableType)resetteable;
            }
            else
            {
                value = (ResetteableType)Activator.CreateInstance(reseteableType);
            }

            value.Assign(parameters);
            return value;
        }

        public void Release<ReseteableType>(ReseteableType resetteable) where ReseteableType : IResetteable
        {
            resetteable.Reset();
            concurrentPool[typeof(ReseteableType)].Push(resetteable);
        }
    }
}
