using System;
using ZooArchitect.Architecture.Entities;
using ZooArchitect.View.Mapping;

namespace ZooArchitect.View.Entities
{
    [ViewOf(typeof(Infrastructure))]
    internal sealed class InfrastructureView : StructureView
    {
        public override Type ArchitectureEntityType => typeof(Infrastructure);
    }
}
