using SBaier.AI;
using SBaier.DI;

namespace SBaier.Astrominer
{
    public class CollectOresAIActionsFactory : AIActionsFactory
    {
        private CollectOresAISettings _collectOresAISettings;
        
        public override void Inject(Resolver resolver)
        {
            base.Inject(resolver);
            _collectOresAISettings = resolver.Resolve<CollectOresAISettings>();
        }
        
        public WeightedNode Create(
            AIBrain brain,
            Observable<bool> allowsFollowupAction,
            BasicLog log)
        {
            // Any ores stored in owned asteroids?
            Node anyOresStoredByAsteroidsCondition = new Condition(() => brain.AnyOresStoredByAsteroids)
                .WithName("Any ores stored by asteroids?")
                .Logged(log, _generalSettings.EnableLogging);

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
            return collectOresWeighted;
        }
    }
}
