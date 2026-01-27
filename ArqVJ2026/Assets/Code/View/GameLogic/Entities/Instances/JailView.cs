using System;
using ZooArchitect.Architecture.Entities;

namespace ZooArchitect.View.Entities
{
    internal sealed class JailView : StructureView
    {
        public override Type ArchitectureEntityType => typeof(Jail);
    }
}
