namespace ZooArchitect.Architecture.GameLogic
{
    public struct Resource
    {
        private string name;
        private long minValue;
        private long maxValue;
        private long startValue;
        private long currentValue;

        public Resource(string name, long minValue, long maxValue, long startValue)
        {
            this.name = name;
            this.minValue = minValue;
            this.maxValue = maxValue;
            this.startValue = startValue;
            this.currentValue = System.Math.Clamp(startValue, minValue, maxValue);
        }

        public string Name => name;
        public long CurrentValue => currentValue;

        public void AddResource(long amount)
        {
            currentValue = System.Math.Clamp(CurrentValue + amount, minValue, maxValue);
        }

        public void RemoveResource(long amount)
        {
            currentValue = System.Math.Clamp(CurrentValue - amount, minValue, maxValue);
        }

        public void SetResourceAmount(long amount)
        {
            currentValue = System.Math.Clamp(amount, minValue, maxValue);
        }
    }
}
