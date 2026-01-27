using ianco99.ToolBox.Events;
using ianco99.ToolBox.Services;
using System;
using System.Reflection;
using UnityEngine;
using ZooArchitect.Architecture.Entities;
using ZooArchitect.Architecture.Entities.Events;
using ZooArchitect.View.Resources;

namespace ZooArchitect.View.Entities
{
    internal sealed class EntityFactoryView : IDisposable
    {
        private EventBus EventBus => ServiceProvider.Instance.GetService<EventBus>();
        private EntityRegistry EntityRegistry => ServiceProvider.Instance.GetService<EntityRegistry>();
        private PrefabsRegistryView PrefabsRegistryView => ServiceProvider.Instance.GetService<PrefabsRegistryView>();
        private MethodInfo registerEntityMethod;

        public EntityFactoryView()
        {
            EventBus.Subscribe<EntityCreatedEvent<Entity>>(OnEntityCreated);

            registerEntityMethod = PrefabsRegistryView.GetType().GetMethod(PrefabsRegistryView.RegisterMethodName, BindingFlags.NonPublic | BindingFlags.Instance);
        }

        private void OnEntityCreated(in EntityCreatedEvent<Entity> callback)
        {
            //TODO Coordinate to 
            GameObject instance = UnityEngine.Object.Instantiate(PrefabsRegistryView.Get(callback.blueprintId), new Vector3((float)callback.coordinate.Origin.X, 0.0f, (float)callback.coordinate.Origin.Y), Quaternion.identity);

            EntityView entityView = instance.GetComponent<EntityView>();
            registerEntityMethod.Invoke(EntityRegistry, new object[] {entityView});
        }

        public void Dispose()
        {
            EventBus.UnSubscribe<EntityCreatedEvent<Entity>>(OnEntityCreated);
        }
    }
}
