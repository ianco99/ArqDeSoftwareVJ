using ianco99.ToolBox.Services;
using System;
using System.Collections.Generic;
using UnityEngine;
using ZooArchitect.Architecture.Controllers;
using ZooArchitect.Architecture.Controllers.Events;
using ZooArchitect.Architecture.Math;
using ZooArchitect.View.Mapping;
using LogicScene = ZooArchitect.Architecture.Scene;

namespace ZooArchitect.View.Controller
{
	[ViewOf(typeof(TerrainModifierControllerArchitecture))]
	internal sealed class TerrainModifierControllerView : GroupSelectionControllerView
	{
		private LogicScene Scene => ServiceProvider.Instance.GetService<LogicScene>();

		public TerrainModifierControllerView()
		{
			EventBus.Subscribe<ModifyTerrainRequestRejectedEvent>(OnModifyTerrainRecuestRejected);
			EventBus.Subscribe<ModifyTerrainRequestAceptedEvent>(OnModifyTerrainAceptedRejected);
		}

		public override void CreateController()
		{
			Dictionary<string, Action> validTilesToSwap = new Dictionary<string, Action>();

			Coordinate selection = new Coordinate(StartGroupClickPosition, FinishGroupClickPosition);

			foreach (string tileID in Scene.GetValidTilesForSelection(selection))
			{
				string currentTileID = tileID;
				validTilesToSwap.Add($"Swap to {currentTileID}", () =>
				{
					EventBus.Raise<ModifyTerrainRequestEvent>(selection.Origin, selection.End, currentTileID);
				});
			}

			Display(validTilesToSwap);
		}

		private void OnModifyTerrainAceptedRejected(in ModifyTerrainRequestAceptedEvent modifyTerrainRequestAceptedEvent)
		{
			FeedbackFactory.SpawnPositiveFeedback(new Vector3(modifyTerrainRequestAceptedEvent.end.x, modifyTerrainRequestAceptedEvent.end.y));
		}

		private void OnModifyTerrainRecuestRejected(in ModifyTerrainRequestRejectedEvent modifyTerrainRequestRejectedEvent)
		{
			FeedbackFactory.SpawnNegativeFeedback(new Vector3(modifyTerrainRequestRejectedEvent.end.x, modifyTerrainRequestRejectedEvent.end.y));
		}

		public override void Dispose()
		{
			EventBus.UnSubscribe<ModifyTerrainRequestRejectedEvent>(OnModifyTerrainRecuestRejected);
			EventBus.UnSubscribe<ModifyTerrainRequestAceptedEvent>(OnModifyTerrainAceptedRejected);
		}
	}
}
