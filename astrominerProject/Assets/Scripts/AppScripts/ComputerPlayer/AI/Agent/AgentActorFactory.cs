using System;
using SBaier.AI;
using SBaier.DI;

namespace SBaier.Astrominer
{
    public class AgentActorFactory : AIActionsFactory, Factory<AgentActor, Ship>, Injectable
    {
        private Factory<AIBrain, Ship> _brainFactory;
        private IdentifyAsteroidAIActionsFactory _identifyAsteroidAIActionsFactory;
        private OccupyAsteroidAIActionsFactory _occupyAsteroidAIActionsFactory;
        private EarnCreditsAIActionsFactory _earnCreditsAIActionsFactory;
        private TakeExploiterAIActionsFactory _takeExploiterAIActionsFactory;
        private SellMachineAIActionsFactory _sellMachineAIActionsFactory;

        public void Inject(Resolver resolver)
        {
            _brainFactory = resolver.Resolve<Factory<AIBrain, Ship>>();
            _identifyAsteroidAIActionsFactory = resolver.Resolve<IdentifyAsteroidAIActionsFactory>();
            _occupyAsteroidAIActionsFactory = resolver.Resolve<OccupyAsteroidAIActionsFactory>();
            _earnCreditsAIActionsFactory = resolver.Resolve<EarnCreditsAIActionsFactory>();
            _takeExploiterAIActionsFactory = resolver.Resolve<TakeExploiterAIActionsFactory>();
            _sellMachineAIActionsFactory = resolver.Resolve<SellMachineAIActionsFactory>();
        }

        public AgentActor Create(Ship ship)
        {
            AIBrain brain = _brainFactory.Create(ship);
            Observable<bool> allowsFollowupAction = new Observable<bool>() { Value = false };
            WeightedSelector selector = CreateActions(brain, allowsFollowupAction);
            return new AgentActor(brain, selector, allowsFollowupAction);
        }

        private WeightedSelector CreateActions(AIBrain brain, Observable<bool> allowsFollowupAction)
        {
            WeightedSelector selector = new WeightedSelector();
            WeightedNode[] childern =
            {
                _identifyAsteroidAIActionsFactory.Create(brain, allowsFollowupAction),
                _occupyAsteroidAIActionsFactory.Create(brain, allowsFollowupAction),
                _earnCreditsAIActionsFactory.Create(brain, allowsFollowupAction),
                _takeExploiterAIActionsFactory.Create(brain, allowsFollowupAction),
                //_sellMachineAIActionsFactory.Create(brain, allowsFollowupAction),
                //CreateIncreaseOreOutputActions(brain, allowsFollowupAction),
                CreateFlyToRandomAsteroidActions(brain, allowsFollowupAction)
            };
            selector.With(childern).WithId(AINodeType.ActionSet).WithName("Action Set");
            return selector;
        }

        private WeightedNode CreateFlyToRandomAsteroidActions(AIBrain brain, Observable<bool> allowsFollowupAction)
        {
            Node action = new FlyToAction(brain, allowsFollowupAction, brain.GetRandomFlyTargetInRange)
                .WithName("Fly to random target in range action").WithId(AINodeType.FlyToRandomTarget);
            WeightedNode result = new WeightedNode(action, new ConstantValueWeighter(0));
            result.WithName("Fly to random target in range");
            return result;
        }
    }
}