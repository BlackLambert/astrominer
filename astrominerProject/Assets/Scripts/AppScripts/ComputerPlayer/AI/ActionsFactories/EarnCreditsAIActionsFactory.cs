using System;
using SBaier.AI;
using SBaier.DI;

namespace SBaier.Astrominer
{
    public class EarnCreditsAIActionsFactory : AIActionsFactory, Injectable
    {
        private EarnCreditsAiSettings _earnCreditsAiSettings;
        private SellOresAISettings _sellOresAISettings;
        private CollectOresAISettings _collectOresAISettings;
        private SendCarrierDroneAISettings _sendDroneAISettings;

        public void Inject(Resolver resolver)
        {
            _earnCreditsAiSettings = resolver.Resolve<EarnCreditsAiSettings>();
            _sellOresAISettings = resolver.Resolve<SellOresAISettings>();
            _collectOresAISettings = resolver.Resolve<CollectOresAISettings>();
            _sendDroneAISettings = resolver.Resolve<SendCarrierDroneAISettings>();
        }

        public WeightedNode Create(
            AIBrain brain,
            Observable<bool> allowsFollowupAction)
        {
            // Conditions
            Selector conditions = new Selector();
            conditions.WithName("Any ways to earn money?");

            // Any ores in ship inventory?
            Node anyOresInShipCondition =
                new Condition(() => brain.AnyOresInShipInventory).WithName("Any ores in ship inventory?");
            conditions.AddChild(anyOresInShipCondition);
            // Any ores stored in owned asteroids?
            Node anyOresStoredByAsteroidsCondition =
                new Condition(() => brain.AnyOresStoredByAsteroids).WithName("Any ores stored by asteroids?");
            conditions.AddChild(anyOresStoredByAsteroidsCondition);


            // Actions
            WeightedSelector actionSelector = new WeightedSelector();

            // Fly to asteroid with highest collect ores prio (weighted)
            Node collectOresAction = new FlyToAction(brain, allowsFollowupAction,
                    () => brain.GetBestCollectTargetFor(CollectOresVesselType.Ship))
                .WithName("Collect ores action");
            Weighter collectOresWeighter = new CollectOresWeighter(_collectOresAISettings, brain);
            WeightedNode collectOresWeighted =
                new WeightedNode(collectOresAction, collectOresWeighter, anyOresStoredByAsteroidsCondition);
            collectOresWeighted.WithName("Collect ores action weighted").WithId(AINodeType.CollectOres);

            // Fly to base to sell ores (weighted)
            Node sellActionSelector = CreateSellOresSelector(brain, allowsFollowupAction);
            Weighter sellOresWeighter = new SellOresWeighter(_sellOresAISettings, brain);
            WeightedNode sellActionsWeighted = new WeightedNode(sellActionSelector, sellOresWeighter,
                anyOresInShipCondition);
            sellActionsWeighted.WithName("Sell ores action weighted").WithId(AINodeType.SellOres);

            // Send carrier drone (weighted)
            Node canBuyCarrierDroneCondition = new CanPurchaseCondition(brain, _earnCreditsAiSettings.CarrierDroneSettings.Price);
            Node sendCarrierDroneConditions =
                CreateSequence(canBuyCarrierDroneCondition, anyOresStoredByAsteroidsCondition)
                    .WithName("Send carrier done conditions");
            Node sendCarrierDrone = new SendCarrierDroneAction(brain, allowsFollowupAction)
                .WithName("Send carrier drone action");
            Weighter sendCarrierDroneWeighter = new SendCarrierDroneWeighter(_sendDroneAISettings, brain);
            WeightedNode sendCarrierDroneWeighted = new WeightedNode(sendCarrierDrone, sendCarrierDroneWeighter,
                sendCarrierDroneConditions);

            actionSelector.AddChildren(new[] { sellActionsWeighted, collectOresWeighted, sendCarrierDroneWeighted });

            EarnCreditsWeighter weighter = new EarnCreditsWeighter(_earnCreditsAiSettings, brain);
            WeightedNode result = new WeightedNode(actionSelector, weighter, conditions);
            result.WithId(AINodeType.EarnMoney).WithName("Earn money motivation");
            return result;
        }

        private Node CreateSellOresSelector(AIBrain brain, Observable<bool> allowsFollowupAction)
        {
            Node shipAtBaseCondition = new Condition(brain.IsShipAtBase).WithName("Is ship at base?");
            Node sellOresAction = new SellOresAction(brain, allowsFollowupAction).WithName("Sell ores action");
            Node sellActionSequence = CreateSequence(shipAtBaseCondition, sellOresAction)
                .WithName("Sell ores");

            Node shipNotAtBaseCondition = new Condition(() => !brain.IsShipAtBase()).WithName("Is ship not at base?");
            Node flyToBaseAction = new FlyToAction(brain, allowsFollowupAction, () => brain.Base)
                .WithName("Fly to base action");
            Node flyToBaseSequence = CreateSequence(shipNotAtBaseCondition, flyToBaseAction)
                .WithName("Fly to base");

            return new Selector().With(sellActionSequence).With(flyToBaseSequence).WithName("Sell action selector");
        }
    }
}