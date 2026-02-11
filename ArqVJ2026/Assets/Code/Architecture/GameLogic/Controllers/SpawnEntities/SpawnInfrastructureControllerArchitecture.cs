using ianco99.ToolBox.Events;
using ianco99.ToolBox.Services;
using System;
using ZooArchitect.Architecture.Controllers.Events;
using ZooArchitect.Architecture.Entities;
using ZooArchitect.Architecture.Math;

namespace ZooArchitect.Architecture.Controllers
{
    public sealed class SpawnInfrastructureControllerArchitecture : IDisposable
    {
        private EventBus EventBus => ServiceProvider.Instance.GetService<EventBus>();
        private EntityRegistry EntityRegistry => ServiceProvider.Instance.GetService<EntityRegistry>();

        public SpawnInfrastructureControllerArchitecture()
        {
            EventBus.Subscribe<SpawnInfrastructureRequestEvent>(RequestSpawnInfrastructure);
        }

        private void RequestSpawnInfrastructure(in SpawnInfrastructureRequestEvent spawnInfrastructureRequestEvent)
        {
            bool collides = false;
            Coordinate tentativeSpawnCoordinate = new Coordinate(spawnInfrastructureRequestEvent.pointToSpawn); ;

            foreach (Structure structure in EntityRegistry.Structures)
            {
                if (structure.coordinate.Overlaps(tentativeSpawnCoordinate))
                {
                    EventBus.Raise<SpawnInfrastructureRequestRejectedEvent>
                        (spawnInfrastructureRequestEvent.blueprintToSpawn, spawnInfrastructureRequestEvent.pointToSpawn);
                    return;
                }
            }

            EventBus.Raise<SpawnInfrastructureRequestAcceptedEvent>
                (spawnInfrastructureRequestEvent.blueprintToSpawn, spawnInfrastructureRequestEvent.pointToSpawn);
        }

        public void Dispose()
        {
            EventBus.UnSubscribe<SpawnInfrastructureRequestEvent>(RequestSpawnInfrastructure);
        }
    }

}
