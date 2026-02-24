using ianco99.ToolBox.Blueprints;
using ianco99.ToolBox.Rules;
using ianco99.ToolBox.Services;
using ZooArchitect.Architecture.Entities.Events;
using ZooArchitect.Architecture.GameLogic;
using ZooArchitect.Architecture.GameLogic.Events;
using ZooArchitect.Architecture.Math;

namespace ZooArchitect.Architecture.Entities
{
    public sealed class Animal : LivingEntity
    {
        public const string PRICE_KEY = "Price";
        public const string PRICE_RESOURCE_KEY = "Price Reource Key";
        public const string CAN_BE_PURCHASED_RULE_KEY = "Can be purchased rule";

        private Wallet Wallet => ServiceProvider.Instance.GetService<Wallet>();
        private RuleFactory RuleFactory => ServiceProvider.Instance.GetService<RuleFactory>();

        [BlueprintParameter("Can be feeded rule")] private string canAnimalBeFeededRuleName;

        private Rule canAnimalBeFeeded;

        [BlueprintParameter("Food needed per day")] private int foodNeededPerDay;
        public int FoodNeededPerDay => foodNeededPerDay;

        [BlueprintParameter("Food resource key")] private string foodResourceKey;
        private string FoodResourceKey => foodResourceKey;

        [BlueprintParameter("Weight")] private int weight;
        public int Weight => weight;

        [BlueprintParameter("Sleep start hour")] private int sleepStartHour;
        public int SleepStartHour => sleepStartHour;

        [BlueprintParameter("Sleep end hour")] private int sleepEndHour;
        public int SleepEndHour => sleepEndHour;

        [BlueprintParameter(PRICE_KEY)] private int price;
        public int Price => price;

        [BlueprintParameter(PRICE_RESOURCE_KEY)] private string priceResourceKey;
        private string PriceResourceKey => priceResourceKey;

        [BlueprintParameter(CAN_BE_PURCHASED_RULE_KEY)] private string canBePurchasedRuleName;
        private string CanBePurchasedRuleName => canBePurchasedRuleName;

        [BlueprintParameter("Incompatible in habitat animals")] private string[] incompatibleInHabitatAnimals;
        public string[] IncompatibleInHabitatAnimals => incompatibleInHabitatAnimals;

        private Animal(uint ID, Coordinate coordinate) : base(ID, coordinate)
        {

        }

        public override void LateInit()
        {
            canAnimalBeFeeded = RuleFactory.GetRule(canAnimalBeFeededRuleName);
            base.LateInit();
        }

        public override void Tick(float deltaTime)
        {
        }

        internal void Feed()
        {
            if (canAnimalBeFeeded.Evaluate(this))
            {
                EventBus.Raise<RemoveResourceToWalletEvent>(foodResourceKey, foodNeededPerDay);
                EventBus.Raise<OnAnimalFeedSucsess>(ID);
            }
            else
            {
                EventBus.Raise<OnAnimalFeedFail>(ID);
            }
        }
    }
}
