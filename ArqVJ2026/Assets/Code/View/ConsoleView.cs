using ianco99.ToolBox.Events;
using ianco99.ToolBox.Services;
using System;
using ZooArchitect.Architecture.Logs.Events;

namespace ZooArchitect.View.Logs
{
    public sealed class ConsoleView : IDisposable
    {
        private EventBus EventBus => ServiceProvider.Instance.GetService<EventBus>();

        public ConsoleView()
        {
            EventBus.Subscribe<ConsoleLogEvent>(LogMessage);
            EventBus.Subscribe<ConsoleLogWarningEvent>(LogWarning);
            EventBus.Subscribe<ConsoleLogErrorEvent>(LogError);
        }

        public void Dispose()
        {
            EventBus.UnSubscribe<ConsoleLogEvent>(LogMessage);
            EventBus.UnSubscribe<ConsoleLogWarningEvent>(LogWarning);
            EventBus.UnSubscribe<ConsoleLogErrorEvent>(LogError);
        }

        private void LogMessage(in ConsoleLogEvent consoleLogEvent)
        {
            UnityEngine.Debug.Log(consoleLogEvent.message);
        }

        private void LogWarning(in ConsoleLogWarningEvent consoleLogEvent)
        {
            UnityEngine.Debug.LogWarning(consoleLogEvent.message);
        }

        private void LogError(in ConsoleLogErrorEvent consoleLogEvent)
        {
            UnityEngine.Debug.LogError(consoleLogEvent.message);
        }

        
    }
}
