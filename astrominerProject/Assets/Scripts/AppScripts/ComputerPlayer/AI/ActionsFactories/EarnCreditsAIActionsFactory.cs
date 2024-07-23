using System;
using SBaier.AI;
using SBaier.DI;

namespace SBaier.Astrominer
{
    public class EarnCreditsAIActionsFactory : AIActionsFactory
    {
        private EarnCreditsAiSettings _earnCreditsAiSettings;
        private SellOresAISettings _sellOresAISettings;
        private CollectOresAISettings _collectOresAISettings;
        private SendCarrierDroneAISettings _sendDroneAISettings;

        public override void Inject(Resolver resolver)
        {
            base.Inject(resolver);
            _earnCreditsAiSettings = resolver.Resolve<EarnCreditsAiSettings>();
            _sellOresAISettings = resolver.Resolve<SellOresAISettings>();
            _collectOresAISettings = resolver.Resolve<CollectOresAISettings>();
            _sendDroneAISettings = resolver.Resolve<SendCarrierDroneAISettings>();
        }

        public WeightedNode Create(
            AIBrain brain,
            Observable<bool> allowsFollowupAction,
            BasicLog log)
        {
            // Conditions
            Selector conditions = new Selector();
            conditions.WithName("Any ways to earn money?")
                .Logged(log, _generalSettings.EnableLogging);

            // Any ores in ship inventory?
            Node anyOresInShipCondition = new Condition(() => brain.AnyOresInShipInventory)
                .WithName("Any ores in ship inventory?")
                .Logged(log, _generalSettings.EnableLogging);
            conditions.AddChild(anyOresInShipCondition);

            // Any ores stored in owned asteroids?
            Node anyOresStoredByAsteroidsCondition = new Condition(() => brain.AnyOresStoredByAsteroids)
                .WithName("Any ores stored by asteroids?")
                .Logged(log, _generalSettings.EnableLogging);
            conditions.AddChildren(new[] { anyOresInShipCondition, anyOresStoredByAsteroidsCondition });

            // Actions
            WeightedSelector actionSelector = new WeightedSelector();

            // Fly to asteroid with highest collect ores prio (weighted)
            Node collectOresAction = new FlyToAction(brain, allowsFollowupAction,
                    () => brain.GetBestCollectTargetFor(CollectOresVesselType.Ship))
                .WithName("Collect ores action")
                .Logged(log, _generalSettings.EnableLogging);
            
            Weighter collectOresWeighter = new CollectOresWeighter(_collectOresAISettings, brain);
            WeightedNode collectOresWeighted =
                new WeightedNode(collectOresAction, collectOresWeighter, anyOresStoredByAsteroidsCondition);
            collectOresWeighted.WithName("Collect ores action weighted")
                .WithId(AINodeType.CollectOres);

            // Fly to base to sell ores (weighted)
            Node sellActionSelector = CreateSellOresSelector(brain, allowsFollowupAction, log);
            Weighter sellOresWeighter = new SellOresWeighter(_sellOresAISettings, brain);
            WeightedNode sellActionsWeighted = new WeightedNode(sellActionSelector, sellOresWeighter,
                anyOresInShipCondition);
            sellActionsWeighted.WithName("Sell ores action weighted")
                .WithId(AINodeType.SellOres);

            // Send carrier drone (weighted)
            Node canBuyCarrierDroneCondition =
                new CanPurchaseCondition(brain, _earnCreditsAiSettings.CarrierDroneSettings.Price);
            Node sendCarrierDroneConditions =
                CreateSequence(canBuyCarrierDroneCondition, anyOresStoredByAsteroidsCondition)
                    .WithName("Send carrier done conditions")
                    .Logged(log, _generalSettings.EnableLogging);

            Node sendCarrierDrone = new SendCarrierDroneAction(brain, allowsFollowupAction)
                .WithName("Send carrier drone action")
                .Logged(log, _generalSettings.EnableLogging);

            Weighter sendCarrierDroneWeighter = new SendCarrierDroneWeighter(_sendDroneAISettings, brain);
            WeightedNode sendCarrierDroneWeighted = new WeightedNode(sendCarrierDrone, sendCarrierDroneWeighter,
                sendCarrierDroneConditions);
            sendCarrierDroneWeighted
                .WithName("Send carrier drone action weighted");

            actionSelector.AddChildren(new[] { sellActionsWeighted, collectOresWeighted, sendCarrierDroneWeighted });

            EarnCreditsWeighter weighter = new EarnCreditsWeighter(_earnCreditsAiSettings, brain);
            WeightedNode result = new WeightedNode(actionSelector, weighter, conditions);
            result.WithId(AINodeType.EarnMoney)
                .WithName("Earn money motivation");

            return result;
        }

        private Node CreateSellOresSelector(AIBrain brain, Observable<bool> allowsFollowupAction, Log log)
        {
            Node shipAtBaseCondition = new Condition(brain.IsShipAtBase)
                .WithName("Is ship at base?")
                .Logged(log, _generalSettings.EnableLogging);

            Node sellOresAction = new SellOresAction(brain, allowsFollowupAction)
                .WithName("Sell ores action")
                .Logged(log, _generalSettings.EnableLogging);

            Node sellActionSequence = CreateSequence(shipAtBaseCondition, sellOresAction)
                .WithName("Sell ores")
                .Logged(log, _generalSettings.EnableLogging);

            Node shipNotAtBaseCondition = new Condition(() => !brain.IsShipAtBase())
                .WithName("Is ship not at base?")
                .Logged(log, _generalSettings.EnableLogging);

            Node flyToBaseAction = new FlyToAction(brain, allowsFollowupAction, () => brain.Base)
                .WithName("Fly to base action")
                .Logged(log, _generalSettings.EnableLogging);

            Node flyToBaseSequence = CreateSequence(shipNotAtBaseCondition, flyToBaseAction)
                .WithName("Fly to base")
                .Logged(log, _generalSettings.EnableLogging);

            return new Selector().With(new[] { sellActionSequence, flyToBaseSequence })
                .WithName("Sell action selector")
                .Logged(log, _generalSettings.EnableLogging);
        }
    }
}