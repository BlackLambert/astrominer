using System;
using SBaier.AI;
using SBaier.DI;

namespace SBaier.Astrominer
{
    public class EarnCreditsAIActionsFactory : AIActionsFactory
    {
        private SellOresAISettings _sellOresAISettings;

        public override void Inject(Resolver resolver)
        {
            base.Inject(resolver);
            _sellOresAISettings = resolver.Resolve<SellOresAISettings>();
        }

        public WeightedNode Create(
            AIBrain brain,
            Observable<bool> allowsFollowupAction,
            BasicLog log)
        {
            // Any ores to sell?
            Node anyOresToSellCondition = new Condition(() => brain.OresToSell.GetTotal() != 0)
                .WithName("Any ores in ship inventory?")
                .Logged(log, _generalSettings.EnableLogging);

            // Fly to base to sell ores (weighted)
            Node sellActionSelector = CreateSellOresSelector(brain, allowsFollowupAction, log);
            Weighter sellOresWeighter = new SellOresWeighter(_sellOresAISettings, brain);
            WeightedNode sellActionsWeighted = new WeightedNode(sellActionSelector, sellOresWeighter,
                anyOresToSellCondition);
            sellActionsWeighted.WithName("Sell ores motivation")
                .WithId(AINodeType.SellOres);

            return sellActionsWeighted;
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