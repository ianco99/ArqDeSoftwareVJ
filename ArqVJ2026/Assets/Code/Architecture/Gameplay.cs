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
        private Scene scene;
        public Gameplay(string blueprintsPath)
        {
            ServiceProvider.Instance.AddService<EventBus>(new EventBus());
            ServiceProvider.Instance.AddService<BlueprintRegistry>(new BlueprintRegistry(blueprintsPath));
            ServiceProvider.Instance.AddService<BlueprintBinder>(new BlueprintBinder());
            ServiceProvider.Instance.AddService<TaskScheduler>(new TaskScheduler());

            scene = new Scene();
        }


        public void Init()
        {
            scene.Init();

            
        }

        public void LateInit()
        {

            scene.LateInit();

            EventBus.Subscribe<EntityCreatedEvent<Entity>>(NewEntityCreated);
            EventBus.Subscribe<EntityCreatedEvent<Animal>>(NewAnimalCreated);
        }




        public void Tick(float deltaTime)
        {
            ServiceProvider.Instance.GetService<Time>();
            time.Tick(deltaTime);
            TaskScheduler.Tick(time.LogicDeltaTime);
            scene.Tick(deltaTime);
        }

        public void Dispose()
        {
            scene.Dispose();
        }

        private void NewEntityCreated(in EntityCreatedEvent<Entity> callback)
        {

        }

        private void NewAnimalCreated(in EntityCreatedEvent<Animal> callback)
        {

        }
    }
}