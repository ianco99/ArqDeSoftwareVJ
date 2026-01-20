using ianco99.ToolBox.Events;
using ianco99.ToolBox.Services;
using ianco99.ToolBox.TaskScheduler;
using ianco99.ToolBox.Updateable;
using ZooArchitect.Architecture.Entities;
using ZooArchitect.Architecture.Entities.Events;
using ZooArchitect.Architecture.GameLogic;
using ZooArchitect.Architecture.Math;

namespace ZooArchitect.Architecture
{
    public sealed class Gameplay : IUpdateable
    {

        private EventBus EventBus => ServiceProvider.Instance.GetService<EventBus>();
        private TaskScheduler TaskScheduler => ServiceProvider.Instance.GetService<TaskScheduler>();
        private Time time => ServiceProvider.Instance.GetService<Time>();


        public Gameplay()
        {
            ServiceProvider.Instance.AddService<EventBus>(new EventBus());
            ServiceProvider.Instance.AddService<TaskScheduler>(new TaskScheduler());
            ServiceProvider.Instance.AddService<Time>(new Time());
            ServiceProvider.Instance.AddService<DayNightCycle>(new DayNightCycle());
            ServiceProvider.Instance.AddService<Wallet>(new Wallet());
            ServiceProvider.Instance.AddService<EntityRegistry>(new EntityRegistry());
            ServiceProvider.Instance.AddService<EntityFactory>(new EntityFactory());

            EventBus.Subscribe<EntityCreatedEvent<Entity>>(NewEntityCreated);
            EventBus.Subscribe<EntityCreatedEvent<Animal>>(NewAnimalCreated);
        }

        private void NewEntityCreated(in EntityCreatedEvent<Entity> callback)
        {

        }

        private void NewAnimalCreated(in EntityCreatedEvent<Animal> callback)
        {

        }

        public void Update(float deltaTime)
        {
            ServiceProvider.Instance.GetService<Time>();
            time.Update(deltaTime);
            TaskScheduler.Update(time.LogicDeltaTime);
            
        }

        public void Init()
        {
            ServiceProvider.Instance.GetService<EntityFactory>().CreateInstance<Animal>(new Coordinate(0, 0));
            //EventBus.Raise<GameInitializedEvent>("sasa");
        }
    }
}