using ianco99.ToolBox.Services;
using System;
using UnityEngine;
using ZooArchitect.Architecture.Entities;

namespace ZooArchitect.View.Entities
{
    internal abstract class EntityView : ViewComponent
    {
        protected EntityRegistry EntityRegistry => ServiceProvider.Instance.GetService<EntityRegistry>();

        public abstract Type ArchitectureEntityType { get; }
        public uint ArchitectureEntityID => architectureEntityID;
        protected uint architectureEntityID;

        protected Entity ArchitectureEntity => EntityRegistry.GetAs<Entity>(architectureEntityID);
    }
}
