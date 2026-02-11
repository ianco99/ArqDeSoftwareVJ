using ianco99.ToolBox.Events;
using ZooArchitect.Architecture.Math;

namespace ZooArchitect.Architecture.Controllers.Events
{
    public struct SpawnInfrastructureRequestAcceptedEvent : IEvent
    {
        public string blueprintToSpawn;
        public Point coordinateToSpawn;

        public void Assign(params object[] parameters)
        {
            blueprintToSpawn = (string)parameters[0];
            coordinateToSpawn = (Point)parameters[1];
        }

        public void Reset()
        {
            blueprintToSpawn = string.Empty;
            coordinateToSpawn = default(Point);
        }
    }
}
