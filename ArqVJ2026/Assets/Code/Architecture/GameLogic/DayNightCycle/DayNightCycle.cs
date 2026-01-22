using ianco99.ToolBox.Events;
using ianco99.ToolBox.Services;
using ianco99.ToolBox.TaskScheduler;
using System.Collections.Generic;
using ianco99.ToolBox.Blueprints;
using ianco99.ToolBox.Bluprints;

namespace ZooArchitect.Architecture.GameLogic
{
    public sealed class DayNightCycle : IService
    {
        private const int DAY_DURATION = 24;
        private const int DAY_STEPS = 6;
        private const int DAY_STEP_DURATION = DAY_DURATION / DAY_STEPS;

        private const int HOUR_DURATION = 60;

        private readonly List<DayStep> daySteps;
        private int currentStep;

        public bool IsPersistance => false;

        private EventBus eventBus => ServiceProvider.Instance.GetService<EventBus>();
        private TaskScheduler taskScheduler => ServiceProvider.Instance.GetService<TaskScheduler>();
        private BlueprintBinder blueprintBinder => ServiceProvider.Instance.GetService<BlueprintBinder>();
        private BlueprintRegistry blueprintRegistry => ServiceProvider.Instance.GetService<BlueprintRegistry>();

        public DayStep CurrentDayStep => daySteps[currentStep];

        public DayNightCycle()
        {
            currentStep = 0;
            daySteps = new List<DayStep>();

            foreach (var VARIABLE in blueprintRegistry.BlueprintsOf("DayNightCycle"))
            {
                object obj = new DayStep();
                blueprintBinder.Apply( ref obj, "DayNightCycle", VARIABLE);

                daySteps.Add(obj as DayStep);
            }

            taskScheduler.Schedule(ChangeStep, DAY_STEP_DURATION * HOUR_DURATION);
        }

        private void ChangeStep()
        {
            currentStep += (currentStep + 1) % daySteps.Count;
            taskScheduler.Schedule(ChangeStep, DAY_STEP_DURATION * HOUR_DURATION);
            eventBus.Raise<DayStepChangeEvent>();
        }
    }

    public sealed class DayStep
    {
        [BlueprintParameter("Name")] public string name;
        [BlueprintParameter("Duration")] public float duration;

        public DayStep()
        {
       
        }
    }
}
