using ianco99.ToolBox.Events;
using ianco99.ToolBox.Services;
using ianco99.ToolBox.TaskScheduler;
using System.Collections.Generic;

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

        public DayStep CurrentDayStep => daySteps[currentStep];

        public DayNightCycle()
        {
            currentStep = 0;
            daySteps = new List<DayStep>();
            daySteps.Add(new DayStep("Mañana", DAY_STEP_DURATION));
            daySteps.Add(new DayStep("Mediodía", DAY_STEP_DURATION));
            daySteps.Add(new DayStep("Tarde", DAY_STEP_DURATION));
            daySteps.Add(new DayStep("Atardecer", DAY_STEP_DURATION));
            daySteps.Add(new DayStep("Anochecer", DAY_STEP_DURATION));
            daySteps.Add(new DayStep("Madrugada", DAY_STEP_DURATION));

            taskScheduler.Schedule(ChangeStep, DAY_STEP_DURATION * HOUR_DURATION);
        }

        private void ChangeStep()
        {
            currentStep += (currentStep + 1) % daySteps.Count;
            taskScheduler.Schedule(ChangeStep, DAY_STEP_DURATION * HOUR_DURATION);
            eventBus.Raise<DayStepChangeEvent>();
        }
    }

    public struct DayStep
    {
        public string name;
        public float duration;

        public DayStep(string name, float duration)
        {
            this.name = name;
            this.duration = duration;
        }
    }
}
