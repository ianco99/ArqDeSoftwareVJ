using ianco99.ToolBox.Events;

namespace ZooArchitect.Architecture.Logs.Events
{
    public struct ConsoleLogErrorEvent : IEvent
    {
        public string message;

        public void Assign(params object[] parameters)
        {
            message = parameters[0] as string;
        }

        public void Reset()
        {
            message = default(string);
        }
    }
}