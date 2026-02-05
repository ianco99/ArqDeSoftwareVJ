using ianco99.ToolBox.Events;
using ianco99.ToolBox.Services;
using ianco99.ToolBox.TaskScheduler;
using System.Collections.Generic;
using ianco99.ToolBox.Blueprints;
using ZooArchitect.Architecture.Data;
using ZooArchitect.Architecture.GameLogic.Events;

namespace ZooArchitect.Architecture.GameLogic
{
    public sealed class DayNightCycle : IService
    {
        private readonly List<DayStep> daySteps;
        private int currentStep;

        public bool IsPersistance => false;
        //TODO BANANEAR
        private EventBus eventBus => ServiceProvider.Instance.GetService<EventBus>();
        private TaskScheduler taskScheduler => ServiceProvider.Instance.GetService<TaskScheduler>();
        private BlueprintBinder blueprintBinder => ServiceProvider.Instance.GetService<BlueprintBinder>();
        private BlueprintRegistry blueprintRegistry => ServiceProvider.Instance.GetService<BlueprintRegistry>();

        public DayStep CurrentDayStep => daySteps[currentStep];

        public DayNightCycle()
        {
            currentStep = 0;
            daySteps = new List<DayStep>();
            
            foreach (var VARIABLE in blueprintRegistry.BlueprintsOf(TableNames.DAY_NIGHT_CYCLE_TABLE_NAME))
            {
                object obj = new DayStep();
                blueprintBinder.Apply(ref obj, TableNames.DAY_NIGHT_CYCLE_TABLE_NAME, VARIABLE);

                daySteps.Add((DayStep)obj);
            }

            taskScheduler.Schedule(ChangeStep, CurrentDayStep.duration);
        }

        private void ChangeStep()
        {
            currentStep += (currentStep + 1) % daySteps.Count;
            taskScheduler.Schedule(ChangeStep, CurrentDayStep.duration);
            eventBus.Raise<DayStepChangeEvent>();

            if(currentStep >= 0)
            {
                eventBus.Raise<DayChangeEvent>();
            }
        }
    }

    public struct DayStep
    {
        [BlueprintParameter("Name")] public string name;
        [BlueprintParameter("Duration")] public float duration;

    }
}
