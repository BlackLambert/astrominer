using System;
using SBaier.AI;
using SBaier.DI;

namespace SBaier.Astrominer
{
    public class SellMachineAIActionsFactory : AIActionsFactory
    {
        private SellMachineAISettings _settings;

        public override void Inject(Resolver resolver)
        {
            base.Inject(resolver);
            _settings = resolver.Resolve<SellMachineAISettings>();
        }

        public WeightedNode Create(AIBrain brain,
            Observable<bool> allowsFollowupAction, BasicLog log)
        {
            Node anyExploiterInInventoryCondition = new Condition(() => brain.HasExploitMachine)
                .WithName("Sell exploiter conditions")
                .Logged(log, _generalSettings.EnableLogging);

            // Sell machine in base
            Node atBaseCondition = new Condition(brain.IsShipAtBase)
                .WithName("Is the location the base?")
                .Logged(log, _generalSettings.EnableLogging);
            Node sellMachineAction = new SellExploiterAction(brain, allowsFollowupAction)
                .WithName("Sell exploiter action")
                .Logged(log, _generalSettings.EnableLogging);
            Node sellMachineSequence = new Sequence().With(new[] { atBaseCondition, sellMachineAction })
                .WithName("Sell machine sequence")
                .Logged(log, _generalSettings.EnableLogging);

            // Fly to base
            Node notBaseCondition = new Condition(() => !brain.IsShipAtBase())
                .WithName("Is the location not the base?")
                .Logged(log, _generalSettings.EnableLogging);
            Node flyToBaseAction = new FlyToAction(brain, allowsFollowupAction, () => brain.Base)
                .WithName("Fly to base action")
                .Logged(log, _generalSettings.EnableLogging);
            Node flyToBaseSequence = new Sequence().With(new[] { notBaseCondition, flyToBaseAction })
                .WithName("Fly to base sequence")
                .Logged(log, _generalSettings.EnableLogging);

            Node selector = new Selector().With(new [] {sellMachineSequence, flyToBaseSequence})
                .WithName("Sell exploiter motivation selector")
                .WithId(AINodeType.SellMachine)
                .Logged(log, _generalSettings.EnableLogging);
            WeightedNode result = new WeightedNode(selector, new SellMachineWeighter(brain, _settings),
                anyExploiterInInventoryCondition);
            result.WithName("Sell exploiter motivation")
                .WithId(AINodeType.SellMachine);
            return result;
        }
    }
}