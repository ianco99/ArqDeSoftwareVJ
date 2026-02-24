using ianco99.ToolBox.Scheduling;
using ianco99.ToolBox.Services;
using ianco99.ToolBox.DataFlow;

namespace ZooArchitect.Architecture.GameLogic
{
    public sealed class Time : IService, ITickable
    {
        public bool IsPersistance => false;

        private float lastDeltaTime;
        private float timeMultiplier;
        public float LogicDeltaTime => lastDeltaTime * timeMultiplier;

        public Time()
        {
            timeMultiplier = 1.0f;
        }

        public void Tick(float deltaTime)
        {
            lastDeltaTime = deltaTime;
        }
    }
}
