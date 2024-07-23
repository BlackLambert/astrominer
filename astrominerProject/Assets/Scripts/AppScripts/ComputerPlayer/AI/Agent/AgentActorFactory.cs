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

        public override void Inject(Resolver resolver)
        {
            base.Inject(resolver);
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
            BasicLog log = new BasicLog();
            Node actions = CreateActions(brain, allowsFollowupAction, log);
            return new AgentActor(brain, actions, allowsFollowupAction, log, _generalSettings.EnableLogging);
        }

        private Node CreateActions(AIBrain brain, Observable<bool> allowsFollowupAction, BasicLog log)
        {
            WeightedSelector selector = new WeightedSelector();
            WeightedNode[] childern =
            {
                _identifyAsteroidAIActionsFactory.Create(brain, allowsFollowupAction, log),
                _occupyAsteroidAIActionsFactory.Create(brain, allowsFollowupAction, log),
                _earnCreditsAIActionsFactory.Create(brain, allowsFollowupAction, log),
                _takeExploiterAIActionsFactory.Create(brain, allowsFollowupAction, log),
                _sellMachineAIActionsFactory.Create(brain, allowsFollowupAction, log),
                //CreateIncreaseOreOutputActions(brain, allowsFollowupAction),
                CreateFlyToRandomAsteroidActions(brain, allowsFollowupAction, log)
            };

            Node result = selector.With(childern)
                .WithId(AINodeType.ActionSet)
                .WithName("Action Set")
                .Logged(log, _generalSettings.EnableLogging)
                .ConsoleLogged(log, _generalSettings.EnableLogging);
            return result;
        }

        private WeightedNode CreateFlyToRandomAsteroidActions(
            AIBrain brain, 
            Observable<bool> allowsFollowupAction,
            Log log)
        {
            Node action = new FlyToAction(brain, allowsFollowupAction, brain.GetRandomFlyTargetInRange)
                .WithName("Fly to random target in range action")
                .WithId(AINodeType.FlyToRandomTarget)
                .Logged(log, _generalSettings.EnableLogging);
            WeightedNode result = new WeightedNode(action, new ConstantValueWeighter(0));
            result.WithName("Fly to random target in range")
                .WithId(AINodeType.FlyToRandomTarget);
            return result;
        }
    }
}