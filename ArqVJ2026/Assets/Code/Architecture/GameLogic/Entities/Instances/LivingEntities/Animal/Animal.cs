using ianco99.ToolBox.Blueprints;
using ZooArchitect.Architecture.Math;

namespace ZooArchitect.Architecture.Entities
{
    public sealed class Animal : LivingEntity
    {
        private Animal(uint ID, Coordinate coordinate) : base(ID, coordinate)
        {

        }
    }
}
