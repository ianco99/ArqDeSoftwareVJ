using ianco99.ToolBox.Events;
using ianco99.ToolBox.Services;
using System;
using ZooArchitect.Architecture.Controllers.Events;

namespace ZooArchitect.Architecture.Controllers
{
    public sealed class TerrainModifierControllerArchitecture : IDisposable
    {
        private EventBus EventBus => ServiceProvider.Instance.GetService<EventBus>();

        public TerrainModifierControllerArchitecture()
        {

            EventBus.Subscribe<ModifyTerrainRequestEvent>(OnModifyTerrainRequest);
        }

        private void OnModifyTerrainRequest(in ModifyTerrainRequestEvent modifyTerrainRequestEvent)
        {
            EventBus.Raise<ModifyTerrainRequestAceptedEvent>(
                modifyTerrainRequestEvent.origin,
                modifyTerrainRequestEvent.end,
                modifyTerrainRequestEvent.newTileId);
        }

        public void Dispose()
        {
            EventBus.UnSubscribe<ModifyTerrainRequestEvent>(OnModifyTerrainRequest);
        }
    }
}