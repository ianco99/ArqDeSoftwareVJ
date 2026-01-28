using ianco99.ToolBox.Blueprints;
using ianco99.ToolBox.DataFlow;
using ianco99.ToolBox.Events;
using ianco99.ToolBox.Services;
using ianco99.ToolBox.TaskScheduler;
using System;
using ZooArchitect.Architecture.Entities;
using ZooArchitect.Architecture.Entities.Events;
using ZooArchitect.Architecture.GameLogic;
using ZooArchitect.Architecture.GameLogic.Controllers;

namespace ZooArchitect.Architecture
{
    public sealed class Gameplay : IInitable, ITickable, IDisposable
    {

        private EventBus EventBus => ServiceProvider.Instance.GetService<EventBus>();
        private TaskScheduler TaskScheduler => ServiceProvider.Instance.GetService<TaskScheduler>();
        private Time time => ServiceProvider.Instance.GetService<Time>();

        private EntityFactory EntityFactory => ServiceProvider.Instance.GetService<EntityFactory>();

        private SpawnEntityControllerArchitecture spawnEntityControllerArchitecture;
        public Gameplay(string blueprintsPath)
        {
            ServiceProvider.Instance.AddService<EventBus>(new EventBus());
            ServiceProvider.Instance.AddService<BlueprintRegistry>(new BlueprintRegistry(blueprintsPath));
            ServiceProvider.Instance.AddService<BlueprintBinder>(new BlueprintBinder());
            ServiceProvider.Instance.AddService<TaskScheduler>(new TaskScheduler());

        }

        private void NewEntityCreated(in EntityCreatedEvent<Entity> callback)
        {

        }

        private void NewAnimalCreated(in EntityCreatedEvent<Animal> callback)
        {

        }

        public void Tick(float deltaTime)
        {
            ServiceProvider.Instance.GetService<Time>();
            time.Tick(deltaTime);
            TaskScheduler.Tick(time.LogicDeltaTime);

        }

        public void Init()
        {
            ServiceProvider.Instance.AddService<Time>(new Time());
            ServiceProvider.Instance.AddService<DayNightCycle>(new DayNightCycle());
            ServiceProvider.Instance.AddService<Wallet>(new Wallet());
            ServiceProvider.Instance.AddService<EntityRegistry>(new EntityRegistry());
            ServiceProvider.Instance.AddService<EntityFactory>(new EntityFactory());

            
        }

        public void LateInit()
        {
            //new Map(10, 11);
            EntityFactory.CreateInstance<Animal>("Monkey", new Math.Coordinate(new Math.Point(0,0)));


            EventBus.Subscribe<EntityCreatedEvent<Entity>>(NewEntityCreated);
            EventBus.Subscribe<EntityCreatedEvent<Animal>>(NewAnimalCreated);
            spawnEntityControllerArchitecture = new SpawnEntityControllerArchitecture();
        }

        public void Dispose()
        {
            spawnEntityControllerArchitecture.Dispose();
        }
    }
}