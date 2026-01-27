using ianco99.ToolBox.DataFlow;
using ianco99.ToolBox.Services;

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
