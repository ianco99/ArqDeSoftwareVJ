using ianco99.ToolBox.Events;
using ZooArchitect.Architecture.Math;

namespace ZooArchitect.Architecture.Controllers.Events
{
    public struct SpawnInfrastructureAcceptedEvent : IEvent
    {
        public Point origin;

        public void Assign(params object[] parameters)
        {
            origin = (Point)parameters[0];
        }

        public void Reset()
        {
            origin = default(Point);
        }
    }
}
