using ianco99.ToolBox.Services;
using ianco99.ToolBox.TaskScheduler;
using ianco99.ToolBox.Updateable;
using System.Collections.Generic;

namespace ZooArchitect.Architecture.GameLogic
{
    public sealed class Time : IService, IUpdateable
    {
        public bool IsPersistance => false;

        private float timeMultiplier;
        private float lastDeltaTime;
        public float LogicDeltaTime => lastDeltaTime * timeMultiplier;

        public Time()
        {
            timeMultiplier = 1.0f;
        }

        public void Update(float deltaTime)
        {
            lastDeltaTime = deltaTime;
        }
    }

    
}
