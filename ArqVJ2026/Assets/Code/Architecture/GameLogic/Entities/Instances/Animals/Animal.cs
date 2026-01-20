using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZooArchitect.Architecture.Math;

namespace ZooArchitect.Architecture.Entities
{
    public sealed class Animal : Entity
    {
        protected Animal(uint ID, Coordinate coordinate) : base(ID, coordinate)
        {

        }
    }
}
