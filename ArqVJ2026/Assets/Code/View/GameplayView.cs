using ianco99.ToolBox.Events;
using ianco99.ToolBox.Services;
using UnityEngine;
using ZooArchitect.Architecture;

namespace ZooArchitect.View
{
    public sealed class GameplayView : MonoBehaviour
    {
        public EventBus EventBus => ServiceProvider.Instance.GetService<EventBus>();

        private Gameplay gameplay;
        void Start()
        {
            gameplay = new Gameplay();
            EventBus.Subscribe<GameInitializedEvent>(OnGameInitialized);
            gameplay.Init();
        }

        void Update()
        {
            gameplay.Update(Time.deltaTime);
        }
        private void OnGameInitialized(GameInitializedEvent gameInitializedEvent)
        {
            Debug.Log("Game initialized " + gameInitializedEvent.name);

        }
    }
}
