using ianco99.ToolBox.Events;
using ianco99.ToolBox.Services;
using System;
using ZooArchitect.Architecture.Controllers.Events;
using ZooArchitect.Architecture.Entities;

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
            foreach (Infrastructure infrastructure in EntityRegistry.Infrastructures)
            {
                if (infrastructure.coordinate.Origin == spawnInfrastructureRequestEvent.coordinateToSpawn.Origin)
                {
                    collides = true;
                    break;
                }
            }

            if (collides)
            {
                EventBus.Raise<SpawnInfrastructureRequestRejectedEvent>
                    (spawnInfrastructureRequestEvent.blueprintToSpawn, spawnInfrastructureRequestEvent.coordinateToSpawn);
            }
            else
            {
                EventBus.Raise<SpawnInfrastructureRequestAcceptedEvent>
                    (spawnInfrastructureRequestEvent.blueprintToSpawn, spawnInfrastructureRequestEvent.coordinateToSpawn);
            }
        }

        public void Dispose()
        {
            EventBus.UnSubscribe<SpawnInfrastructureRequestEvent>(RequestSpawnInfrastructure);
        }
    }

}
