using ianco99.ToolBox.Events;

namespace ZooArchitect.Architecture
{
    public struct GameInitializedEvent : IEvent
    {
        public string name;

        public void Assign(params object[] parameters)
        {
            name = parameters[0] as string;
        }

        public void Reset()
        {
            name = default;
        }
    }

}
