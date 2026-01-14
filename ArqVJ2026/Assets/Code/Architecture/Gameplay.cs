using ianco99.ToolBox.Events;
using ianco99.ToolBox.Services;
using ianco99.ToolBox.TaskScheduler;
using ianco99.ToolBox.Updateable;

namespace ZooArchitect.Architecture
{
    public sealed class Gameplay : IUpdateable
    {

        EventBus EventBus => ServiceProvider.Instance.GetService<EventBus>();

        public Gameplay()
        {
            ServiceProvider.Instance.AddService<EventBus>(new EventBus());
            ServiceProvider.Instance.AddService<TaskScheduler>(new TaskScheduler());
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