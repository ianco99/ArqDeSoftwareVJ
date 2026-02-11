using ianco99.ToolBox.Events;
using ianco99.ToolBox.Services;
using System;
using ZooArchitect.Architecture.Controllers.Events;
using ZooArchitect.Architecture.Entities;
using ZooArchitect.Architecture.Logs;
using ZooArchitect.Architecture.Math;

namespace ZooArchitect.Architecture.GameLogic.Controllers
{
    public sealed class SpawnJailControllerArchitecture : IDisposable
    {
        private EventBus EventBus => ServiceProvider.Instance.GetService<EventBus>();
        private EntityRegistry EntityRegistry => ServiceProvider.Instance.GetService<EntityRegistry>();
        public SpawnJailControllerArchitecture()
        {
            EventBus.Subscribe<SpawnJailRequestEvent>(RequestSpawnJail);
        }

        private void RequestSpawnJail(in SpawnJailRequestEvent spawnJailRequestEvent)
        {
            Coordinate tentativeNewCoordinate = new Coordinate(spawnJailRequestEvent.origin, spawnJailRequestEvent.end);


            foreach (Structure structure in EntityRegistry.Structures)
            {
                if (structure.coordinate.Overlaps(tentativeNewCoordinate))
                {
                    EventBus.Raise<SpawnJailRequestRejectedEvent>(spawnJailRequestEvent.origin, spawnJailRequestEvent.end, spawnJailRequestEvent.blueprintName);
                    GameConsole.Warning("STRUCTURE OVERLAPS!!");
                    return;
                }
            }

            foreach (Jail jail in EntityRegistry.Jails)
            {
                if (jail.coordinate.IsInInner(tentativeNewCoordinate))
                {
                    EventBus.Raise<SpawnJailRequestAcceptedEvent>(spawnJailRequestEvent.origin, spawnJailRequestEvent.end, spawnJailRequestEvent.blueprintName);
                    return;
                }
            }

                    EventBus.Raise<SpawnJailRequestRejectedEvent>(spawnJailRequestEvent.origin, spawnJailRequestEvent.end, spawnJailRequestEvent.blueprintName);

        }

        public void Dispose()
        {
            EventBus.UnSubscribe<SpawnJailRequestEvent>(RequestSpawnJail);
        }
    }
}
