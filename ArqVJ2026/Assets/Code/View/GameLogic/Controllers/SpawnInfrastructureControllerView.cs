using ianco99.ToolBox.Events;
using System;
using System.Collections.Generic;
using ZooArchitect.Architecture.Controllers.Events;
using ZooArchitect.Architecture.Logs;
using ZooArchitect.Architecture.Math;

namespace ZooArchitect.View.Controller
{
    internal sealed class SpawnInfrastructureControllerView : SingleSelectionControllerView
    {
        public SpawnInfrastructureControllerView()
        {
            EventBus.Subscribe<SpawnInfrastructureRequestEvent>(OnSpawnInfrastructureRejected);
        }

        private void OnSpawnInfrastructureRejected(in SpawnInfrastructureRequestEvent spawnInstrasftructureRequestRejectedEvent)
        {

        }

        protected override Dictionary<string, Action> GetActionsToDsiplay(Coordinate clickPoint, List<string> blueprints)
        {
            Dictionary<string, Action> spawnEntities = new Dictionary<string, Action>();
            for (int i = 0; i < blueprints.Count; i++)
            {
                int index = i;
                spawnEntities.Add($"Build {blueprints[index]}", () =>
                {
                    EventBus.Raise<SpawnInfrastructureRequestEvent>(blueprints[index], clickPoint);
                });
            }

            return spawnEntities;
        }

        protected override List<string> GetValidBlueprints(Coordinate clickpoint)
        {
            return new List<string>();
        }

        public override void Dispose()
        {
            EventBus.UnSubscribe<SpawnInfrastructureRequestEvent>(OnSpawnInfrastructureRejected);
        }


    }
}