using System;
using System.Collections.Generic;
using ZooArchitect.Architecture.Math;
using ianco99.ToolBox.Services;
using LogicScene = ZooArchitect.Architecture.Scene;
namespace ZooArchitect.View.Controller
{
    internal sealed class TerrainModifierControllerView : GroupSelectionControllerView
    {
        private LogicScene Scene => ServiceProvider.Instance.GetService<LogicScene>();

        public TerrainModifierControllerView()
        {
        }

        public override void CreateController()
        {
            Dictionary<string, Action> validTilesToSwap = new Dictionary<string, Action>();

            Coordinate selection = new Coordinate(StartGroupClickPosition, FinishGroupClickPosition);

            foreach (string tileID in Scene.GetValidTilesForSelection(selection))
            {
                validTilesToSwap.Add($"Swap to {tileID}", () =>
                {
                    //EventBus.Raise<SpawnEntityRequestEvent>(animalsBlueprints[index], clickPoint);
                });
            }

            Display(validTilesToSwap);
        }


        public override void Dispose()
        {
        }
    }
}
