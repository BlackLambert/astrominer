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
            // Sell machine in base
            Node anyExploiterInInventoryCondition = new Condition(() => brain.HasExploitMachine)
                .WithName("Sell exploiter conditions");

            WeightedSelector selector = new WeightedSelector();
            selector.WithName("Sell exploiter motivation");
            selector.WithId(AINodeType.SellMachine);
            return new WeightedNode(selector, new SellMachineWeighter(brain, _settings), anyExploiterInInventoryCondition);
        }
    }
}