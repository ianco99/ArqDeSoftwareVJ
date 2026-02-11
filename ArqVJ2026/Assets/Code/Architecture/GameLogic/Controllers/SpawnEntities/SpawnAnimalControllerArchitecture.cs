using ianco99.ToolBox.Events;
using ianco99.ToolBox.Services;
using System;
using ZooArchitect.Architecture.Controllers.Events;
using ZooArchitect.Architecture.Entities;
using ZooArchitect.Architecture.Math;

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
            Coordinate tentativeSpawnCoordinate = new Coordinate(spawnAnimalRequestEvent.pointToSpawn);
            bool collides = false;

            foreach (Animal animal in EntityRegistry.Animals)
            {
                if (animal.coordinate.Overlaps(tentativeSpawnCoordinate))
                {
                    collides = true;
                    break;
                }
            }

            foreach (Jail jail in EntityRegistry.Jails)
            {
                if(jail.coordinate.Overlaps(tentativeSpawnCoordinate))
                {

                }
            }

            if (collides)
            {
                EventBus.Raise<SpawnAnimalRequestRejectedEvent>(spawnAnimalRequestEvent.blueprintToSpawn, spawnAnimalRequestEvent.pointToSpawn);
            }
            else
            {
                EventBus.Raise<SpawnAnimalRequestAcceptedEvent>(spawnAnimalRequestEvent.blueprintToSpawn, spawnAnimalRequestEvent.pointToSpawn);
            }
        }

        public void Dispose()
        {
            EventBus.UnSubscribe<SpawnAnimalRequestEvent>(RequestAnimalEntity);
        }
    }
}
