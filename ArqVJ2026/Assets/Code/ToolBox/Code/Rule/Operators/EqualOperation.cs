namespace ianco99.ToolBox.Rules
{
    [RuleOperator("==")]
    public sealed class EqualOperation : RuleOperation 
    {
        public override bool Evaluate(int a, int b)
        {
            return a == b;
        }
    }
}
