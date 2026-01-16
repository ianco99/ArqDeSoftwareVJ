using ianco99.ToolBox.Events;
using ianco99.ToolBox.Services;
using ianco99.ToolBox.TaskScheduler;
using ianco99.ToolBox.Updateable;
using ZooArchitect.Architecture.GameLogic;
using ZooArchitect.Architecture.Logs;

namespace ZooArchitect.Architecture
{
    public sealed class Gameplay : IUpdateable
    {

        private EventBus EventBus => ServiceProvider.Instance.GetService<EventBus>();
        private TaskScheduler TaskScheduler => ServiceProvider.Instance.GetService<TaskScheduler>();
        private Time time => ServiceProvider.Instance.GetService<Time>();
        private DayNightCycle dayNightCycle => ServiceProvider.Instance.GetService<DayNightCycle>();


        public Gameplay()
        {
            ServiceProvider.Instance.AddService<EventBus>(new EventBus());
            ServiceProvider.Instance.AddService<TaskScheduler>(new TaskScheduler());
            ServiceProvider.Instance.AddService<Time>(new Time());
            ServiceProvider.Instance.AddService<DayNightCycle>(new DayNightCycle());
            ServiceProvider.Instance.AddService<Wallet>(new Wallet());
        }


        public void Update(float deltaTime)
        {
            ServiceProvider.Instance.GetService<Time>();
            time.Update(deltaTime);
            TaskScheduler.Update(time.LogicDeltaTime);
            
        }

        public void Init()
        {
            EventBus.Raise<GameInitializedEvent>("sasa");
        }
    }
}