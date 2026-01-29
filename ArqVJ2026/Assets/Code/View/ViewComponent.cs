using ianco99.ToolBox.DataFlow;
using System;
using UnityEngine;

namespace ZooArchitect.View
{
    internal abstract class ViewComponent : MonoBehaviour, IInitable, ITickable, IDisposable
    {
        public virtual void Init() { }

        public virtual void LateInit() { }

        public virtual void Tick(float deltaTime) { }

        public virtual void Dispose() { }
    }
}
