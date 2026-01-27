using ianco99.ToolBox.Events;
using ianco99.ToolBox.Services;
using ianco99.ToolBox.TaskScheduler;
using UnityEngine;
using ZooArchitect.Architecture;
using ZooArchitect.Architecture.GameLogic;
using ZooArchitect.View.Entities;
using ZooArchitect.View.Logs;
using ZooArchitect.View.Mapping;
using ZooArchitect.View.Resources;

namespace ZooArchitect.View
{
    [ViewOf(typeof(Gameplay))]
    public sealed class GameplayView : MonoBehaviour
    {
        public EventBus EventBus => ServiceProvider.Instance.GetService<EventBus>();
        public TaskScheduler TaskScheduler => ServiceProvider.Instance.GetService<TaskScheduler>();

        private string BlueprintsPath => System.IO.Path.Combine(Application.streamingAssetsPath, "Blueprints", "Blueprints.xlsx");

        private Gameplay gameplay;
        private ConsoleView consoleView;
        EntityFactoryView entityFactoryView;

        void Start()
        {

            EventBus.Subscribe<DayStepChangeEvent>(OnStepChanged);
            gameplay.Init();
            gameplay.LateInit();

        }

        private void Awake()
        {
            ViewToArchitectureMap.Init();

            gameplay = new Gameplay(BlueprintsPath);

            ServiceProvider.Instance.AddService<EntityRegistryView>(new EntityRegistryView());
            ServiceProvider.Instance.AddService<PrefabsRegistryView>(new PrefabsRegistryView());
            entityFactoryView = new EntityFactoryView();

            consoleView = new ConsoleView();

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
