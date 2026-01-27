using ianco99.ToolBox.Events;
using ianco99.ToolBox.Services;
using ianco99.ToolBox.TaskScheduler;
using UnityEngine;
using ZooArchitect.Architecture;
using ZooArchitect.Architecture.GameLogic;
using ZooArchitect.View.Logs;

namespace ZooArchitect.View
{
    public sealed class GameplayView : MonoBehaviour
    {
        public EventBus EventBus => ServiceProvider.Instance.GetService<EventBus>();
        public TaskScheduler TaskScheduler => ServiceProvider.Instance.GetService<TaskScheduler>();

        private string BlueprintsPath => System.IO.Path.Combine(Application.streamingAssetsPath, "Blueprints", "Blueprints.xlsx");

        private Gameplay gameplay;
        private ConsoleView consoleView;

        void Start()
        {
            gameplay = new Gameplay(BlueprintsPath);
            consoleView = new ConsoleView();


        }

        private void Awake()
        {
            EventBus.Subscribe<DayStepChangeEvent>(OnStepChanged);

            gameplay.Init();
            gameplay.LateInit();
        }

        void Update()
        {
            gameplay.Tick(UnityEngine.Time.deltaTime);
        }

        private void OnDisable()
        {
            consoleView.Dispose();

            EventBus.UnSubscribe<DayStepChangeEvent>(OnStepChanged);

        }

        private void OnStepChanged(in DayStepChangeEvent gameInitializedEvent)
        {
            Debug.Log("CHANGED STEP");

        }
    }
}
