using System;

namespace ianco99.ToolBox.Rules
{
    public sealed class RuleOperatorAttribute : Attribute 
    {
        public string operatorKey;

        public RuleOperatorAttribute(string operatorKey)
        {
            this.operatorKey = operatorKey;
        }
    }
}
