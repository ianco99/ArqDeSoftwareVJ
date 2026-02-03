using ianco99.ToolBox.DataFlow;
using ianco99.ToolBox.Events;
using ianco99.ToolBox.Services;
using ZooArchitect.Architecture.Entities.Events;
using ZooArchitect.Architecture.Math;

namespace ZooArchitect.Architecture.Entities
{
    public abstract class Entity : IInitable, ITickable
    {
        public const uint UNASSIGNED_ENTITY_ID = 0;
        public uint ID;
        public Coordinate coordinate;

        protected EventBus EventBus => ServiceProvider.Instance.GetService<EventBus>();

        protected Entity(uint ID, Coordinate coordinate)
        {
            this.ID = ID;
            this.coordinate = coordinate;
        }

        public virtual void Init() { }

        public virtual void LateInit(){ }

        public virtual void Tick(float deltaTime) { }

        public void Move(Point offset)
        {
            coordinate.Move(offset);
            EventBus.Raise<EntityMovedEvent>(ID);
        }

    }
}
