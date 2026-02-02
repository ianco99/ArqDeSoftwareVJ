using ianco99.ToolBox.Services;
using System;
using UnityEngine;
using ZooArchitect.Architecture.Entities;
using ZooArchitect.View.Mapping;
using ZooArchitect.View.Scene;

namespace ZooArchitect.View.Entities
{
    [ViewOf(typeof(Entity))]
    internal abstract class EntityView : ViewComponent
    {
        protected EntityRegistry EntityRegistry => ServiceProvider.Instance.GetService<EntityRegistry>();

        public abstract Type ArchitectureEntityType { get; }

        public static string SetIdMethodName => nameof(SetId);
        private void SetId(uint ID)
        {
            architectureEntityID = ID;
        }

        public uint ArchitectureEntityID => architectureEntityID;
        protected uint architectureEntityID;

        protected Entity ArchitectureEntity => EntityRegistry.GetAs<Entity>(architectureEntityID);
    }
}
