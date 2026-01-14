using ianco99.ToolBox.Events;
using ianco99.ToolBox.Services;

namespace ZooArchitect.Architecture
{
    public sealed class Gameplay : IUpdateable
    {

        EventBus EventBus => ServiceProvider.Instance.GetService<EventBus>();

        public Gameplay()
        {
            ServiceProvider.Instance.AddService<EventBus>(new EventBus());
        }


        public void Update(float deltaTime)
        {
        }

        public void Init()
        {
            EventBus.Raise<GameInitializedEvent>("sasa");
        }
    }
}