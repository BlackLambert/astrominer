using System.Linq;
using SBaier.AI;
using SBaier.DI;

namespace SBaier.Astrominer
{
    public class OccupyAsteroidAIActionsFactory : AIActionsFactory
    {
        private OccupyAsteroidAiSettings _aiSettings;

        public override void Inject(Resolver resolver)
        {
            base.Inject(resolver);
            _aiSettings = resolver.Resolve<OccupyAsteroidAiSettings>();
        }

        public WeightedNode Create(AIBrain brain,
            Observable<bool> allowsFollowupAction, BasicLog log)
        {
            // Conditions
            // Money to by exploiter or exploiter in inventory
            // Is there any identified asteroid to occupy?

            Selector moneyOrExploiterSelector = new Selector();
            moneyOrExploiterSelector.WithName("Has means to exploit?");

            Node canPurchaseAnyExploitMachine =
                new Condition(() => brain.CanAfford(brain.GetPriceOfLeastExpensiveExploiter()))
                    .WithName("Can purchase any exploit machine?")
                    .Logged(log, _generalSettings.EnableLogging);
            
            Node hasExploiterCondition = new Condition(() => brain.HasExploitMachine)
                .WithName("Has any exploiter in the inventory?")
                .Logged(log, _generalSettings.EnableLogging);
            
            moneyOrExploiterSelector.AddChildren(new[] { canPurchaseAnyExploitMachine, hasExploiterCondition });

            Node anyIdentifiedAsteroidCondition = new Condition(() => brain.AnyIdentifiedUnoccupiedAsteroid)
                .WithName("Is there any identified asteroid?")
                .Logged(log, _generalSettings.EnableLogging);
            
            Node conditions = CreateSequence(moneyOrExploiterSelector, anyIdentifiedAsteroidCondition)
                .WithName("Occupy asteroid conditions")
                .Logged(log, _generalSettings.EnableLogging);

            Selector selector = new Selector();
            selector.WithId(AINodeType.OccupyAsteroid)
                .WithName("Occupy asteroid selector")
                .Logged(log, _generalSettings.EnableLogging);

            // Place Exploiter
            selector.AddChild(CreatePlaceExploiterAction(brain, allowsFollowupAction, log));
            // Fly to target asteroid
            selector.AddChild(CreateFlyToTargetAsteroidAction(brain, allowsFollowupAction, log));
            // Buy Exploiter
            selector.AddChild(CreateBuyExploiterAction(brain, allowsFollowupAction, log));
            // Fly to base
            selector.AddChild(CreateFlyToBaseAction(brain, allowsFollowupAction, log));

            OccupyAsteroidWeighter weighter = new OccupyAsteroidWeighter(_aiSettings, brain);
            WeightedNode result = new WeightedNode(selector, weighter, conditions);
            result.WithId(AINodeType.OccupyAsteroid)
                .WithName("Occupy asteroid motivation");
            return result;
        }

        private Node CreatePlaceExploiterAction(AIBrain brain, Observable<bool> allowsFollowupAction, BasicLog log)
        {
            // Already at target location
            Node isAtLocationCondition = new Condition(brain.IsShipAtOccupationTarget)
                .WithName("Is the ship location the occupation target?")
                .Logged(log, _generalSettings.EnableLogging);
            // Exploiter in inventory
            Node anyExploiterInInventoryCondition = new Condition(() => brain.HasExploitMachine)
                .WithName("Is there any exploiter in the inventory")
                .Logged(log, _generalSettings.EnableLogging);
            Node action = new PlaceExploiterAction(brain, allowsFollowupAction)
                .WithName("Place exploiter action")
                .Logged(log, _generalSettings.EnableLogging);
            Node result = CreateSequence(anyExploiterInInventoryCondition, isAtLocationCondition, action)
                .WithName("Place exploiter")
                .Logged(log, _generalSettings.EnableLogging);
            return result;
        }

        private Node CreateFlyToTargetAsteroidAction(AIBrain brain, Observable<bool> allowsFollowupAction, BasicLog log)
        {
            // Not already there
            Node isNotAlreadyThereCondition = new Condition(() => !brain.IsShipAtOccupationTarget())
                .WithName("Is the ship at a different location?")
                .Logged(log, _generalSettings.EnableLogging);
            // Any exploiter in the inventory
            Node anyExploiterInInventoryCondition = new Condition(() => brain.HasExploitMachine)
                .WithName("Is any exploiter in the inventory?")
                .Logged(log, _generalSettings.EnableLogging);
            Node action = new FlyToAction(brain, allowsFollowupAction, () => brain.UnoccupiedAsteroidWithBestValue)
                .WithName("Fly to occupation target action")
                .Logged(log, _generalSettings.EnableLogging);
            Node result = CreateSequence(anyExploiterInInventoryCondition, isNotAlreadyThereCondition, action)
                .WithName("Fly to occupation target")
                .Logged(log, _generalSettings.EnableLogging);
            return result;
        }

        private Node CreateBuyExploiterAction(AIBrain brain, Observable<bool> allowsFollowupAction, BasicLog log)
        {
            // At base
            Node isAtBaseCondition = new Condition(brain.IsShipAtBase)
                .WithName("Is the ship at the base?");
            // Enough money to buy exploiter
            Node canPurchaseAnyExploitMachine = CreatePurchaseAnyExploitMachineCondition(brain, log);
            // Empty inventory space?
            Node hasEmptyInventorySpace = CreateHasInventorySpaceCondition(brain, log);
            Node action = new BuyExploiterAction(brain, allowsFollowupAction)
                .WithName("Buy exploiter action")
                .Logged(log, _generalSettings.EnableLogging);
            Node result = CreateSequence(hasEmptyInventorySpace, canPurchaseAnyExploitMachine, 
                    isAtBaseCondition, action)
                .WithName("Buy exploiter")
                .Logged(log, _generalSettings.EnableLogging);
            return result;
        }

        private Node CreateFlyToBaseAction(AIBrain brain, Observable<bool> allowsFollowupAction, BasicLog log)
        {
            // Conditions
            // Not already there
            Node isAtAwayFromBaseCondition = new Condition(() => !brain.IsShipAtBase())
                .WithName("Is the ship not at the base?")
                .Logged(log, _generalSettings.EnableLogging);
            // Enough money to buy exploiter
            Node canPurchaseAnyExploitMachine = CreatePurchaseAnyExploitMachineCondition(brain, log);
            // Empty inventory space?
            Node hasEmptyInventorySpace = CreateHasInventorySpaceCondition(brain, log);
            Node action = new FlyToAction(brain, allowsFollowupAction, () => brain.Base)
                .WithName("Fly to base action")
                .Logged(log, _generalSettings.EnableLogging);
            Node result = CreateSequence(hasEmptyInventorySpace, canPurchaseAnyExploitMachine, 
                isAtAwayFromBaseCondition, action)
                .WithName("Fly to base")
                .Logged(log, _generalSettings.EnableLogging);
            return result;
        }

        private Node CreatePurchaseAnyExploitMachineCondition(AIBrain brain, Log log)
        {
            return new Condition(() => brain.CanAfford(brain.GetPriceOfLeastExpensiveExploiter()))
                .WithName("Can purchase any exploit machine?")
                .Logged(log, _generalSettings.EnableLogging);
        }

        private Node CreateHasInventorySpaceCondition(AIBrain brain, Log log)
        {
            return new Condition(() => brain.HasEmptyInventorySpace)
                .WithName("Any empty inventory space?")
                .Logged(log, _generalSettings.EnableLogging);
        }
    }
}