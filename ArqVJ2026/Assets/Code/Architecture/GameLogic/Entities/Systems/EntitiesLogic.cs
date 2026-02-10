using ianco99.ToolBox.Blueprints;
using ianco99.ToolBox.DataFlow;
using ianco99.ToolBox.Events;
using ianco99.ToolBox.Services;
using System;
using System.Collections.Generic;
using ZooArchitect.Architecture.Data;
using ZooArchitect.Architecture.GameLogic;
using ZooArchitect.Architecture.GameLogic.Events;
using ZooArchitect.Architecture.Math;

namespace ZooArchitect.Architecture.Entities
{
    public sealed class EntitiesLogic : IService, ITickable, IDisposable
    {
        public bool IsPersistance => throw new NotImplementedException();
        private BlueprintRegistry BlueprintRegistry => ServiceProvider.Instance.GetService<BlueprintRegistry>();
        private Scene Scene => ServiceProvider.Instance.GetService<Scene>();
        private EntityRegistry EntityRegistry => ServiceProvider.Instance.GetService<EntityRegistry>();
        private EventBus EventBus => ServiceProvider.Instance.GetService<EventBus>();

        AnimalsLogic animalsLogic;

        public EntitiesLogic()
        {
            animalsLogic = new AnimalsLogic();
            EventBus.Subscribe<DayChangeEvent>(OnDayChange);
        }

        private void OnDayChange(in DayChangeEvent callback)
        {
            animalsLogic.FeedAnimals();
        }
           

        public void Tick(float deltaTime)
        {
            foreach (Entity entity  in EntityRegistry.Animals)
            {
                entity.Tick(deltaTime);
            }

            animalsLogic.Tick(deltaTime);
        }

        public void Dispose()
        {
            EventBus.UnSubscribe<DayChangeEvent>(OnDayChange);
        }

        public List<string> ValidAnimalsToSpawnIn(Coordinate coordinate)
        {
            if(Scene.IsCoordinateInsideMap(coordinate))
            {
                return BlueprintRegistry.BlueprintsOf(TableNames.ANIMALS_TABLE_NAME);
            }
            return new List<string>();
        }
    }
}