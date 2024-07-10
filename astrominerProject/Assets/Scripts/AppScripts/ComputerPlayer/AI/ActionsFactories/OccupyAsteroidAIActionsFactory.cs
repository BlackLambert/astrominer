using System.Linq;
using SBaier.AI;
using SBaier.DI;

namespace SBaier.Astrominer
{
    public class OccupyAsteroidAIActionsFactory : AIActionsFactory, Injectable
    {
        private OccupyAsteroidAiSettings _aiSettings;

        public void Inject(Resolver resolver)
        {
            _aiSettings = resolver.Resolve<OccupyAsteroidAiSettings>();
        }

        public WeightedNode Create(
            AIBrain brain,
            Observable<bool> allowsFollowupAction)
        {
            // Conditions
            // Money to by exploiter or exploiter in inventory
            // Is there any identified asteroid to occupy?

            Selector moneyOrExploiterSelector = new Selector();
            moneyOrExploiterSelector.WithName("Has means to exploit?");

            Node canPurchaseAnyExploitMachine =
                new Condition(() => brain.CanAfford(brain.GetPriceOfLeastExpensiveExploiter()))
                    .WithName("Can purchase any exploit machine?");
            Node hasExploiterCondition = new Condition(() => brain.HasExploitMachine)
                .WithName("Has any exploiter in the inventory?");
            moneyOrExploiterSelector.AddChildren(new[] { canPurchaseAnyExploitMachine, hasExploiterCondition });

            Node anyIdentifiedAsteroidCondition = new Condition(() => brain.AnyIdentifiedUnoccupiedAsteroid)
                .WithName("Is there any identified asteroid?");
            Node conditions = CreateSequence(moneyOrExploiterSelector, anyIdentifiedAsteroidCondition)
                .WithName("Occupy asteroid conditions");

            Selector selector = new Selector();
            selector.WithId(AINodeType.OccupyAsteroid).WithName("Occupy asteroid selector");

            // Place Exploiter
            selector.AddChild(CreatePlaceExploiterAction(brain, allowsFollowupAction));
            // Fly to target asteroid
            selector.AddChild(CreateFlyToTargetAsteroidAction(brain, allowsFollowupAction));
            // Buy Exploiter
            selector.AddChild(CreateBuyExploiterAction(brain, allowsFollowupAction));
            // Fly to base
            selector.AddChild(CreateFlyToBaseAction(brain, allowsFollowupAction));

            OccupyAsteroidWeighter weighter = new OccupyAsteroidWeighter(_aiSettings, brain);
            WeightedNode result = new WeightedNode(selector, weighter, conditions);
            result.WithId(AINodeType.OccupyAsteroid).WithName("Occupy asteroid motivation");
            return result;
        }

        private Node CreatePlaceExploiterAction(AIBrain brain, Observable<bool> allowsFollowupAction)
        {
            // Already at target location
            Node isAtLocationCondition = new Condition(brain.IsShipAtOccupationTarget)
                .WithName("Is the ship location the occupation target?");
            // Exploiter in inventory
            Node anyExploiterInInventoryCondition = new Condition(() => brain.HasExploitMachine)
                .WithName("Is there any exploiter in the inventory");
            Node action = new PlaceExploiterAction(brain, allowsFollowupAction).WithName("Place exploiter action");
            Node result = CreateSequence(anyExploiterInInventoryCondition, isAtLocationCondition, action)
                .WithName("Place exploiter");
            return result;
        }

        private Node CreateFlyToTargetAsteroidAction(AIBrain brain, Observable<bool> allowsFollowupAction)
        {
            // Not already there
            Node isNotAlreadyThereCondition = new Condition(() => !brain.IsShipAtOccupationTarget())
                .WithName("Is the ship at a different location?");
            // Any exploiter in the inventory
            Node anyExploiterInInventoryCondition = new Condition(() => brain.HasExploitMachine)
                .WithName("Is any exploiter in the inventory?");
            Node action = new FlyToAction(brain, allowsFollowupAction, () => brain.UnoccupiedAsteroidWithBestValue)
                .WithName("Fly to occupation target action");
            Node result = CreateSequence(anyExploiterInInventoryCondition, isNotAlreadyThereCondition, action)
                .WithName("Fly to occupation target");
            return result;
        }

        private Node CreateBuyExploiterAction(AIBrain brain, Observable<bool> allowsFollowupAction)
        {
            // At base
            Node isAtBaseCondition = new Condition(brain.IsShipAtBase)
                .WithName("Is the ship at the base?");
            // Enough money to buy exploiter
            Node canPurchaseAnyExploitMachine = CreatePurchaseAnyExploitMachineCondition(brain);
            // Empty inventory space?
            Node hasEmptyInventorySpace = CreateHasInventorySpaceCondition(brain);
            Node action = new BuyExploiterAction(brain, allowsFollowupAction)
                .WithName("Buy exploiter action");
            Node result = CreateSequence(hasEmptyInventorySpace, canPurchaseAnyExploitMachine, 
                    isAtBaseCondition, action).WithName("Buy exploiter");
            return result;
        }

        private Node CreateFlyToBaseAction(AIBrain brain, Observable<bool> allowsFollowupAction)
        {
            // Conditions
            // Not already there
            Node isAtAwayFromBaseCondition = new Condition(() => !brain.IsShipAtBase())
                .WithName("Is the ship not at the base?");
            // Enough money to buy exploiter
            Node canPurchaseAnyExploitMachine = CreatePurchaseAnyExploitMachineCondition(brain);
            // Empty inventory space?
            Node hasEmptyInventorySpace = CreateHasInventorySpaceCondition(brain);
            Node action = new FlyToAction(brain, allowsFollowupAction, () => brain.Base)
                .WithName("Fly to base action");
            Node result = CreateSequence(hasEmptyInventorySpace, canPurchaseAnyExploitMachine, 
                isAtAwayFromBaseCondition, action).WithName("Fly to base");
            return result;
        }

        private Node CreatePurchaseAnyExploitMachineCondition(AIBrain brain)
        {
            return new Condition(() => brain.CanAfford(brain.GetPriceOfLeastExpensiveExploiter()))
                .WithName("Can purchase any exploit machine?");
        }

        private Node CreateHasInventorySpaceCondition(AIBrain brain)
        {
            return new Condition(() => brain.HasEmptyInventorySpace)
                .WithName("Any empty inventory space?");
        }
    }
}