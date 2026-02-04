using ianco99.ToolBox.Events;

namespace ZooArchitect.Architecture.GameLogic
{
    public struct AddResourceToWalletEvent : IEvent
    {
        public string resourceName;
        public long amount;

        public void Assign(params object[] parameters)
        {
            resourceName = (string)parameters[0];
            amount = (long)parameters[1];
        }

        public void Reset()
        {
            resourceName = default(string);
            amount = default(long);
        }
    }
}
