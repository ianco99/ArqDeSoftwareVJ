using ianco99.ToolBox.Events;
using ZooArchitect.Architecture.Math;

namespace ZooArchitect.Architecture.Controllers.Events
{
    public struct SpawnRequestAcceptedEvent<EntityType> : IEvent 
    {
        public string blueprintToSpawn;
        public Coordinate coordinateToSpawn;
        public string blueprintTable;

        public void Assign(params object[] parameters)
        {
            blueprintToSpawn = (string)parameters[0];
            coordinateToSpawn = (Coordinate)parameters[1];
            blueprintTable = (string)parameters[2];
        }

        public void Reset()
        {
            blueprintToSpawn = string.Empty;
            coordinateToSpawn = default(Coordinate);
            blueprintTable = default(string);
        }
    }
}
