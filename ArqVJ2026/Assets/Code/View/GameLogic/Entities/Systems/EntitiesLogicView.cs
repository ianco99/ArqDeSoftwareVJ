using ianco99.ToolBox.DataFlow;
using ianco99.ToolBox.Events;
using ianco99.ToolBox.Services;
using System;
using ZooArchitect.Architecture.Entities;
using ZooArchitect.Architecture.Entities.Events;
using ZooArchitect.View.Mapping;

namespace ZooArchitect.View.Entities
{
    [ViewOf(typeof(EntitiesLogic))]
    internal sealed class EntitiesLogicView : IInitable, ITickable, IDisposable
    {
        private EventBus EventBus => ServiceProvider.Instance.GetService<EventBus>();

        private EntityRegistryView EntityRegistryView => ServiceProvider.Instance.GetService<EntityRegistryView>();
        private EntityRegistry EntityRegistry => ServiceProvider.Instance.GetService<EntityRegistry>();

        public EntitiesLogicView()
        {
            EventBus.Subscribe<EntityMovedEvent>(OnEntityMoved);
        }

        private void OnEntityMoved(in EntityMovedEvent entityMovedEvent)
        {
            EntityRegistryView.GetAs<EntityView>(entityMovedEvent.movedEntityId).
                Move(EntityRegistry.GetAs<Entity>(entityMovedEvent.movedEntityId).coordinate);
        }
        public void Init()
        {
        }

        public void LateInit()
        {
        }

        public void Tick(float deltaTime)
        {

        }

        public void Dispose()
        {
            EventBus.UnSubscribe<EntityMovedEvent>(OnEntityMoved);
        }

    }
}