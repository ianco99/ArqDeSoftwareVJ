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
            EventBus.Subscribe<ConsoleWarningEvent>(LogWarning);
            EventBus.Subscribe<ConsoleErrorEvent>(LogError);
        }

        private void LogMessage(in ConsoleLogEvent consoleLogEvent)
        {
            UnityEngine.Debug.Log(consoleLogEvent.message);
        }

        private void LogWarning(in ConsoleWarningEvent consoleWarningEvent)
        {
            UnityEngine.Debug.LogWarning(consoleWarningEvent.message);
        }

        private void LogError(in ConsoleErrorEvent consoleErrorEvent)
        {
            UnityEngine.Debug.LogError(consoleErrorEvent.message);
        }

        public void Dispose()
        {
            EventBus.UnSubscribe<ConsoleLogEvent>(LogMessage);
            EventBus.UnSubscribe<ConsoleWarningEvent>(LogWarning);
            EventBus.UnSubscribe<ConsoleErrorEvent>(LogError);
        }
    }
}
