using System;
using ZooArchitect.Architecture.Entities;

namespace ZooArchitect.View.Entities
{
    internal abstract class StructureView : EntityView
    {
        public override Type ArchitectureEntityType => typeof(Structure);
    }
}
