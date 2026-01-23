using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ZooArchitect.Architecture.Exceptions
{
    public sealed class DataEntryException : Exception
    {
        public DataEntryException(string info) : base(info)
        {

        }
    }
}
