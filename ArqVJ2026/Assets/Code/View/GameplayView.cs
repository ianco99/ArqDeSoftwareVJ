using ianco99.ToolBox.Events;
using ianco99.ToolBox.Services;
using ianco99.ToolBox.TaskScheduler;
using System.Threading.Tasks;
using UnityEngine;
using ZooArchitect.Architecture;

namespace ZooArchitect.View
{
    public sealed class GameplayView : MonoBehaviour
    {
        public EventBus EventBus => ServiceProvider.Instance.GetService<EventBus>();
        public ianco99.ToolBox.TaskScheduler.TaskScheduler TaskScheduler => ServiceProvider.Instance.GetService<ianco99.ToolBox.TaskScheduler.TaskScheduler>();

        private Gameplay gameplay;

        void Start()
        {
            gameplay = new Gameplay();
            TaskScheduler.Schedule(DoSomething, 3.0f);
            EventBus.Subscribe<GameInitializedEvent>(OnGameInitialized);
            gameplay.Init();
        }

        private void DoSomething()
        {
            ParallelOptions sasa = new ParallelOptions();
            Debug.Log("SASOMETRO : " +  sasa.TaskScheduler.MaximumConcurrencyLevel);
        }

        void Update()
        {
            gameplay.Update(Time.deltaTime);
            TaskScheduler.Update(Time.deltaTime);
        }
        private void OnGameInitialized(GameInitializedEvent gameInitializedEvent)
        {
            Debug.Log("Game initialized " + gameInitializedEvent.name);

        }
    }
}
