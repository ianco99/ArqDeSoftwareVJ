using ianco99.ToolBox.Events;
using ZooArchitect.Architecture.Math;

namespace ZooArchitect.Architecture.Controllers.Events
{
    public struct SpawnJailRequestEvent : IEvent
    {
        public Point origin;
        public Point end;

        public void Assign(params object[] parameters)
        {
            origin= (Point)parameters[0];
            end= (Point)parameters[1];
        }

        public void Reset()
        {
            origin = default(Point);
            end = default(Point);
        }
    }
}
