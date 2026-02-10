using ianco99.ToolBox.Events;
using ianco99.ToolBox.Services;
using System;
using ZooArchitect.Architecture.Controllers.Events;
using ZooArchitect.Architecture.Entities;

namespace ZooArchitect.Architecture.GameLogic.Controllers
{
    public sealed class SpawnAnimalControllerArchitecture : IDisposable
    {
        private EventBus EventBus => ServiceProvider.Instance.GetService<EventBus>();
        private EntityRegistry EntityRegistry => ServiceProvider.Instance.GetService<EntityRegistry>();
        public SpawnAnimalControllerArchitecture()
        {
            EventBus.Subscribe<SpawnAnimalRequestEvent>(RequestAnimalEntity);
        }

        private void RequestAnimalEntity(in SpawnAnimalRequestEvent spawnAnimalRequestEvent)
        {
            bool collides = false;
            foreach (Animal animal in EntityRegistry.Animals)
            {
                if (animal.coordinate.Origin == spawnAnimalRequestEvent.coordinateToSpawn.Origin)
                {
                    collides = true;
                    break;
                }
            }

            if (collides)
            {
                EventBus.Raise<SpawnAnimalRequestRejectedEvent>(spawnAnimalRequestEvent.blueprintToSpawn, spawnAnimalRequestEvent.coordinateToSpawn);
            }
            else
            {
                EventBus.Raise<SpawnAnimalRequestAcceptedEvent>(spawnAnimalRequestEvent.blueprintToSpawn, spawnAnimalRequestEvent.coordinateToSpawn);
            }
        }

        public void Dispose()
        {
            EventBus.UnSubscribe<SpawnAnimalRequestEvent>(RequestAnimalEntity);
        }
    }
}
