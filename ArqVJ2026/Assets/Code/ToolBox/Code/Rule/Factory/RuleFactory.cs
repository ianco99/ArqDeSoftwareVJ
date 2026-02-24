using ianco99.ToolBox.Data;
using ianco99.ToolBox.Services;
using ianco99.ToolBox.Blueprints;

namespace ianco99.ToolBox.Rules
{
    public sealed class RuleFactory : IService
    {
        private BlueprintBinder BlueprintBinder => ServiceProvider.Instance.GetService<BlueprintBinder>();
        public bool IsPersistance => true;

        public RuleFactory() { }

        public Rule GetRule(string ruleBlueprintId)
        {
            object newRule = new Rule();
            BlueprintBinder.Apply(ref newRule, TableNamesToolbox.RULES_TABLE_NAME, ruleBlueprintId);
            (newRule as Rule).Init();
            (newRule as Rule).LateInit();
            return newRule as Rule;
        }
    }
}
