using ianco99.ToolBox.Events;
using ianco99.ToolBox.Services;
using System;
using System.Collections.Generic;

namespace ZooArchitect.Architecture.GameLogic
{
    public sealed class Wallet : IService, IDisposable
    {
        private EventBus EventBus = ServiceProvider.Instance.GetService<EventBus>();
        private readonly Dictionary<string, Resource> resources;
        public bool IsPersistance => false;

        public Wallet()
        {
            EventBus.Subscribe<AddResourceToWalletEvent>(AddResource);
            EventBus.Subscribe<RemoveResourceToWalletEvent>(RemoveResource);

            resources = new Dictionary<string, Resource>();

            CreateResource(new Resource("Plata", 0, long.MaxValue, 1000));
            CreateResource(new Resource("Comida de Animales", 0, long.MaxValue, 50));
            CreateResource(new Resource("Comida de Visintes", 0, long.MaxValue, 50));
            CreateResource(new Resource("Limpieza", 0, 100, 100));
            CreateResource(new Resource("Reputación", 0, long.MaxValue, 800));
            CreateResource(new Resource("Trabajadores", 0, 500, 3));
            CreateResource(new Resource("Animales", 0, 500, 0));

            void CreateResource(Resource resource)
            {
                resources.Add(resource.Name, resource);
            }
        }

        private void RemoveResource(in RemoveResourceToWalletEvent removeResourceToWalletEvent)
        {
            resources[removeResourceToWalletEvent.resourceName].RemoveResource(removeResourceToWalletEvent.amount);
        }

        private void AddResource(in AddResourceToWalletEvent addResourceToWalletEvent)
        {
            resources[addResourceToWalletEvent.resourceName].AddResource(addResourceToWalletEvent.amount);
        }

        public void AddResource(string resource, long amount)
        {
            resources[resource].AddResource(amount);
        }

        internal void RemoveResource(string resource, long amount)
        {
            resources[resource].RemoveResource(amount);
        }

        internal bool HasResourceAmount(string resource, long amount)
        {
            return resources[resource].CurrentValue >= amount;
        }

        public void Dispose()
        {
            EventBus.UnSubscribe<AddResourceToWalletEvent>(AddResource);
            EventBus.UnSubscribe<RemoveResourceToWalletEvent>(RemoveResource);
        }
    }
}
