using System;
using ZooArchitect.Architecture.Entities;

namespace ZooArchitect.View.Entities
{
    internal sealed class InfrastructureView : StructureView
    {
        public override Type ArchitectureEntityType => typeof(Infrastructure);
    }
}
