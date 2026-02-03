using ianco99.ToolBox.Blueprints;
using ZooArchitect.Architecture.Math;

namespace ZooArchitect.Architecture.Entities
{
    public sealed class Animal : LivingEntity
    {
        private float current = 0.0f;
        private float delay = 1.0f;

        private bool up = false;

        //[BlueprintParameter("Food needed per day")] private uint foodNeededPerDay;
        private Animal(uint ID, Coordinate coordinate) : base(ID, coordinate)
        {

        }

        public override void Tick(float deltaTime)
        {
            current += deltaTime;

            if (current >= delay)
            {
                if (up)
                {
                    Move(Point.Up);
                }
                else
                {
                    Move(Point.Right);
                }

                up = !up;
                current = 0;
            }
        }
    }
}
