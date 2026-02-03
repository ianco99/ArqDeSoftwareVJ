using ianco99.ToolBox.Blueprints;
using ianco99.ToolBox.DataFlow;
using ianco99.ToolBox.Services;
using System;
using System.Collections.Generic;
using ZooArchitect.Architecture.Data;
using ZooArchitect.Architecture.Math;

namespace ZooArchitect.Architecture.Entities
{
    public sealed class EntitiesLogic : IService, ITickable, IDisposable
    {
        public bool IsPersistance => throw new NotImplementedException();
        private BlueprintRegistry BlueprintRegistry => ServiceProvider.Instance.GetService<BlueprintRegistry>();
        private Scene Scene => ServiceProvider.Instance.GetService<Scene>();
        private EntityRegistry EntityRegistry => ServiceProvider.Instance.GetService<EntityRegistry>();
        public void Tick(float deltaTime)
        {
            foreach (Entity entity  in EntityRegistry.Animals)
            {
                entity.Tick(deltaTime);
            }
        }

        public void Dispose()
        {
        }

        public List<string> ValidEntitiesToSpawnIn(Coordinate coordinate)
        {
            if(Scene.IsCoordinateInsideMap(coordinate))
            {
                return BlueprintRegistry.BlueprintsOf(TableNames.ANIMALS_TABLE_NAME);
            }
            return new List<string>();
        }
    }
}