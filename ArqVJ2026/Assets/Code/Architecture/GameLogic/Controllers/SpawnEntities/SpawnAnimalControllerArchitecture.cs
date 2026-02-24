using ianco99.ToolBox.Blueprints;
using ianco99.ToolBox.Events;
using ianco99.ToolBox.Rules;
using ianco99.ToolBox.Services;
using System;
using ZooArchitect.Architecture.Controllers.Events;
using ZooArchitect.Architecture.Data;
using ZooArchitect.Architecture.Entities;
using ZooArchitect.Architecture.GameLogic;
using ZooArchitect.Architecture.GameLogic.Events;
using ZooArchitect.Architecture.Math;

namespace ZooArchitect.Architecture.Controllers
{
    public sealed class SpawnAnimalControllerArchitecture : IDisposable
    {
        private EventBus EventBus => ServiceProvider.Instance.GetService<EventBus>();
        private EntityRegistry EntityRegistry => ServiceProvider.Instance.GetService<EntityRegistry>();
        private BlueprintRegistry BlueprintRegistry => ServiceProvider.Instance.GetService<BlueprintRegistry>();
        private Wallet Wallet => ServiceProvider.Instance.GetService<Wallet>();
        private RuleFactory RuleFactory => ServiceProvider.Instance.GetService<RuleFactory>();

        public SpawnAnimalControllerArchitecture()
        {
            EventBus.Subscribe<SpawnRequestEvent<Animal>>(RequestSpawnAnimal);
        }

        private void RequestSpawnAnimal(in SpawnRequestEvent<Animal> spawnAnimalRequestEvent)
        {

            string canBuyRuleName = BlueprintRegistry[TableNames.ANIMALS_TABLE_NAME,
                spawnAnimalRequestEvent.blueprintToSpawn, Animal.CAN_BE_PURCHASED_RULE_KEY];

            Rule canBuyAnimalRule = RuleFactory.GetRule(canBuyRuleName);

            string resourcePurchaseKey = BlueprintRegistry[TableNames.ANIMALS_TABLE_NAME,
                                                    spawnAnimalRequestEvent.blueprintToSpawn, Animal.PRICE_RESOURCE_KEY];

            long price = Convert.ToInt64(BlueprintRegistry[TableNames.ANIMALS_TABLE_NAME,
                                            spawnAnimalRequestEvent.blueprintToSpawn, Animal.PRICE_KEY]);

            if (!canBuyAnimalRule.Evaluate(spawnAnimalRequestEvent.blueprintToSpawn, spawnAnimalRequestEvent.blueprintToSpawn))
            {
                EventBus.Raise<SpawnRequestRejectedEvent<Animal>>
                        (spawnAnimalRequestEvent.blueprintToSpawn, spawnAnimalRequestEvent.coordinateToSpawn,
                        $"{spawnAnimalRequestEvent.blueprintToSpawn} price: {price} - " +
                        $"Money in Wallet: {Wallet.GetResourceAmount(resourcePurchaseKey)} " +
                        $"So much expensive! ");
                return;
            }

            foreach (Animal animal in EntityRegistry.Animals)
            {
                if (animal.coordinate.Overlaps(spawnAnimalRequestEvent.coordinateToSpawn))
                {
                    EventBus.Raise<SpawnRequestRejectedEvent<Animal>>
                        (spawnAnimalRequestEvent.blueprintToSpawn, spawnAnimalRequestEvent.coordinateToSpawn,
                        $"New {spawnAnimalRequestEvent.blueprintToSpawn} overlaps whit {animal.ToString()} " +
                        $"in coordinate {spawnAnimalRequestEvent.coordinateToSpawn.ToString()}");
                    return;
                }
            }

            foreach (Jail jail in EntityRegistry.Jails)
            {
                if (jail.coordinate.IsInInner(spawnAnimalRequestEvent.coordinateToSpawn))
                {
                    EventBus.Raise<RemoveResourceToWalletEvent>(resourcePurchaseKey, price);
                    EventBus.Raise<SpawnRequestAcceptedEvent<Animal>>
                        (spawnAnimalRequestEvent.blueprintToSpawn, spawnAnimalRequestEvent.coordinateToSpawn, TableNames.ANIMALS_TABLE_NAME);
                    return;
                }
            }

            EventBus.Raise<SpawnRequestRejectedEvent<Animal>>
                (spawnAnimalRequestEvent.blueprintToSpawn, spawnAnimalRequestEvent.coordinateToSpawn,
                $"{spawnAnimalRequestEvent.blueprintToSpawn} outside a valid Jail");
        }

        public void Dispose()
        {
            EventBus.UnSubscribe<SpawnRequestEvent<Animal>>(RequestSpawnAnimal);
        }
    }
}
