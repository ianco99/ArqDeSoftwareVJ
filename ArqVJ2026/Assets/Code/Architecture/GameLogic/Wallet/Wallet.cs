using ianco99.ToolBox.Services;
using System.Collections.Generic;

namespace ZooArchitect.Architecture.GameLogic
{
    class Wallet : IService
    {
        private readonly Dictionary<string, Resource> resources;
        public bool IsPersistance => false;

        public Wallet()
        {
            resources = new Dictionary<string, Resource>();

            AddResource(new Resource("Plata", 0, long.MaxValue, 1000));
            AddResource(new Resource("Comida de Animales", 0, long.MaxValue, 50));
            AddResource(new Resource("Comida de Visintes", 0, long.MaxValue, 50));
            AddResource(new Resource("Limpieza", 0, 100, 100));
            AddResource(new Resource("Reputación", 0, long.MaxValue, 800));
            AddResource(new Resource("Trabajadores", 0, 500, 3));
            AddResource(new Resource("Animales", 0, 500, 0));

            void AddResource(Resource resource)
            {
                resources.Add(resource.Name, resource);
            }
        }

    }

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
