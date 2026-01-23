using System;
using System.Runtime.Serialization;

namespace ZooArchitect.Architecture.Exceptions
{
    public sealed class BrokenGameRuleException : Exception
    {
        public BrokenGameRuleException(string info) : base(info)
        {

        }
    }
}
