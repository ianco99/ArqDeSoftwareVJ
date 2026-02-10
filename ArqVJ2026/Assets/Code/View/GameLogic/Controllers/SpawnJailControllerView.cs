using System;
using ZooArchitect.Architecture.Controllers.Events;
using ZooArchitect.Architecture.GameLogic.Controllers;
using ZooArchitect.Architecture.Logs;
using ZooArchitect.View.Mapping;

namespace ZooArchitect.View.Controller
{
    [ViewOf(typeof(SpawnJailControllerArchitecture))]
    internal abstract class SpawnJailControllerView : GroupSelectionControllerView
    {
        public SpawnJailControllerView()
        {
            EventBus.Subscribe<SpawnJailRequestRejectedEvent>(OnSpawnJailRequestRejected);
        }



        private void OnSpawnJailRequestRejected(in SpawnJailRequestRejectedEvent callback)
        {
            GameConsole.Log("Spawn jail rejected! Reason mmesage: " + callback.message);
        }

        public override void CreateController()
        {
            EventBus.Raise<SpawnJailRequestEvent>(StartGroupClickPosition, FinishGroupClickPosition);
        }

        public override void Dispose()
        {
            EventBus.UnSubscribe<SpawnJailRequestRejectedEvent>(OnSpawnJailRequestRejected);
        }
    }
}
