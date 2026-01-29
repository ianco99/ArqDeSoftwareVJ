using ianco99.ToolBox.Blueprints;
using ianco99.ToolBox.DataFlow;
using ianco99.ToolBox.Events;
using ianco99.ToolBox.Services;
using System.Collections.Generic;
using UnityEngine;
using ZooArchitect.Architecture.Controllers.Events;
using ZooArchitect.Architecture.Data;
using ZooArchitect.Architecture.Logs;
using ZooArchitect.Architecture.Math;

namespace ZooArchitect.View.Controller
{
    class SpawnEntityControllerView : ITickable, System.IDisposable
    {
        private readonly List<KeyCode> keys = new()
        {
            KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3, KeyCode.Alpha4, KeyCode.Alpha5
        };

        private EventBus EventBus => ServiceProvider.Instance.GetService<EventBus>();
        private BlueprintRegistry BlueprintRegistry => ServiceProvider.Instance.GetService<BlueprintRegistry>();


        private List<string> animalsBlueprints;

        public SpawnEntityControllerView()
        {
            animalsBlueprints = BlueprintRegistry.BlueprintsOf(TableNames.ANIMALS_TABLE_NAME);
            EventBus.Subscribe<SpawnEntityRequestRejectedEvent>(OnSpawnRejected);
        }


        public void Tick(float deltaTime)
        {
            for (int i = 0; i < animalsBlueprints.Count; i++)
            {
                if(!Input.GetKey(keys[i]))
                {
                    EventBus.Raise<SpawnEntityRequestEvent>(animalsBlueprints[i], new  Coordinate(new Point(i,i)));
                }
            }
        }
        private void OnSpawnRejected(in SpawnEntityRequestRejectedEvent spawnEntityRequestRejectedEVent)
        {
            Console.Log($"Spawn of {spawnEntityRequestRejectedEVent.blueprintToSpawn} in {spawnEntityRequestRejectedEVent.coordinateToSpawn} rejected");
        }

        public void Dispose()
        {
            EventBus.UnSubscribe<SpawnEntityRequestRejectedEvent>(OnSpawnRejected);
        }
    }
}
