using System;
using SBaier.AI;
using SBaier.DI;

namespace SBaier.Astrominer
{
    public class SellMachineAIActionsFactory : AIActionsFactory, Injectable
    {
        private SellMachineAISettings _settings;

        public void Inject(Resolver resolver)
        {
            _settings = resolver.Resolve<SellMachineAISettings>();
        }

        public WeightedNode Create(
            AIBrain brain,
            Observable<bool> allowsFollowupAction)
        {
            Node anyExploiterInInventoryCondition = new Condition(() => brain.HasExploitMachine)
                .WithName("Sell exploiter conditions");

            // Sell machine in base
            Node atBaseCondition = new Condition(brain.IsShipAtBase)
                .WithName("Is the location the base?");
            Node sellMachineAction = new SellExploiterAction(brain, allowsFollowupAction)
                .WithName("Sell exploiter action");
            Node sellMachineSequence = new Sequence().With(new[] { atBaseCondition, sellMachineAction })
                .WithName("Sell machine sequence");

            // Fly to base
            Node notBaseCondition = new Condition(() => !brain.IsShipAtBase())
                .WithName("Is the location not the base?");
            Node flyToBaseAction = new FlyToAction(brain, allowsFollowupAction, () => brain.Base)
                .WithName("Fly to base action");
            Node flyToBaseSequence = new Sequence().With(new[] { notBaseCondition, flyToBaseAction })
                .WithName("Fly to base sequence");

            Node selector = new Selector().With(new [] {sellMachineSequence, flyToBaseSequence})
                .WithName("Sell exploiter motivation")
                .WithId(AINodeType.SellMachine);
            return new WeightedNode(selector, new SellMachineWeighter(brain, _settings),
                anyExploiterInInventoryCondition);
        }
    }
}