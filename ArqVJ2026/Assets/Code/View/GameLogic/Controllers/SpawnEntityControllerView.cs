using ianco99.ToolBox.Blueprints;
using ianco99.ToolBox.DataFlow;
using ianco99.ToolBox.Events;
using ianco99.ToolBox.Services;
using System;
using System.Collections.Generic;
using UnityEngine;
using ZooArchitect.Architecture.Controllers.Events;
using ZooArchitect.Architecture.Data;
using ZooArchitect.Architecture.Logs;
using ZooArchitect.Architecture.Math;
using ZooArchitect.View.Mapping;
using ZooArchitect.View.Scene;

namespace ZooArchitect.View.Controller
{
    [ViewOf(typeof(SpawnEntityControllerView))]
    class SpawnEntityControllerView : ITickable, System.IDisposable
    {
        private EventBus EventBus => ServiceProvider.Instance.GetService<EventBus>();
        private BlueprintRegistry BlueprintRegistry => ServiceProvider.Instance.GetService<BlueprintRegistry>();

        private ContextMenuView ContextMenuView => ServiceProvider.Instance.GetService<ContextMenuView>();

        private List<string> animalsBlueprints;

        public SpawnEntityControllerView()
        {
            animalsBlueprints = BlueprintRegistry.BlueprintsOf(TableNames.ANIMALS_TABLE_NAME);
            EventBus.Subscribe<SpawnEntityRequestRejectedEvent>(OnSpawnRejected);
        }


        public void Tick(float deltaTime)
        {
            if(Input.GetMouseButtonDown(1))
            {
                Dictionary<string, Action> spawnEntities = new Dictionary<string, Action>();

                for (int i = 0; i < animalsBlueprints.Count; i++)
                {
                    int index = i;
                    spawnEntities.Add($"Spawn {animalsBlueprints[index]}", () =>
                    {
                        EventBus.Raise<SpawnEntityRequestEvent>(animalsBlueprints[index], new Coordinate(new Point(index, index)));
                    });

                }

                ContextMenuView.Show(spawnEntities);
            }
        }

        private void OnSpawnRejected(in SpawnEntityRequestRejectedEvent spawnEntityRequestRejectedEVent)
        {
            GameConsole.Log($"Spawn of {spawnEntityRequestRejectedEVent.blueprintToSpawn} in {spawnEntityRequestRejectedEVent.coordinateToSpawn} rejected");
        }

        public void Dispose()
        {
            EventBus.UnSubscribe<SpawnEntityRequestRejectedEvent>(OnSpawnRejected);
        }
    }
}
