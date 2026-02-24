using ianco99.ToolBox.Blueprints;
using ZooArchitect.Architecture.Math;

namespace ZooArchitect.Architecture.Entities
{
    public sealed class Infrastructure : Structure 
    {
        [BlueprintParameter("Name")] private string name;
		public string Name  => name;

        [BlueprintParameter("Maintenance needed per day")] private string maintenanceNeededPerDay;

        private Infrastructure(uint ID, Coordinate coordinate) : base(ID, coordinate)
        {

        }
	}
}
