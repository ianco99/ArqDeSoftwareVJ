using ianco99.ToolBox.Blueprints;
using ianco99.ToolBox.Services;
using ZooArchitect.Architecture.Entities.Events;
using ZooArchitect.Architecture.GameLogic;
using ZooArchitect.Architecture.Math;

namespace ZooArchitect.Architecture.Entities
{
    public sealed class Animal : LivingEntity
    {
        private Wallet Wallet => ServiceProvider.Instance.GetService<Wallet>();
        private float current = 0.0f;
        private float delay = 1.0f;

        private bool up = false;

        [BlueprintParameter("Food needed per day")] private long foodNeededPerDay;
        public long FoodNeededPerDay => foodNeededPerDay;

        private Animal(uint ID, Coordinate coordinate) : base(ID, coordinate)
        {

        }

        internal void Feed()
        {
            if(Wallet.HasResourceAmount("Comida de Animales", foodNeededPerDay))
            {
                EventBus.Raise<RemoveResourceToWalletEvent>("Comida de Animales", foodNeededPerDay);
                EventBus.Raise<OnAnimalFeedSuccess>(ID);
            }
            else
            {
                EventBus.Raise<OnAnimalFeedFail>(ID);
            }
        }

        public override void Tick(float deltaTime)
        {

        }
    }
}
