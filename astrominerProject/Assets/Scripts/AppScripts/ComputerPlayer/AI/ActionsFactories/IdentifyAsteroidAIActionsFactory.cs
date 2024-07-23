using System;
using SBaier.AI;
using SBaier.DI;

namespace SBaier.Astrominer
{
    public class IdentifyAsteroidAIActionsFactory : AIActionsFactory
    {
        private IdentifyAsteroidAISettings _aiSettings;
        private SendProspectorDroneAISettings _sendProspectorDroneSettings;
        private FlyToUnidentifiedAsteroidAISettings _flyToUnidentifiedAsteroidAISettings;
        private DroneBuyer<ProspectorDrone> _droneBuyer;

        public override void Inject(Resolver resolver)
        {
            base.Inject(resolver);
            _sendProspectorDroneSettings = resolver.Resolve<SendProspectorDroneAISettings>();
            _droneBuyer = resolver.Resolve<DroneBuyer<ProspectorDrone>>();
            _aiSettings = resolver.Resolve<IdentifyAsteroidAISettings>();
            _flyToUnidentifiedAsteroidAISettings = resolver.Resolve<FlyToUnidentifiedAsteroidAISettings>();
        }

        public WeightedNode Create(
            AIBrain brain,
            Observable<bool> allowsFollowupAction,
            BasicLog log)
        {
            Node anyUnidentifiedAsteroidsCondition = new Condition(() => brain.HasUnidentifiedAsteroid)
                .WithName("Is there any unidentified asteroid?")
                .Logged(log, _generalSettings.EnableLogging);

            WeightedSelector selector = new WeightedSelector();
            Node selectorLogged = selector.WithId(AINodeType.IdentifyAsteroid)
                .WithName("Identify asteroid selector")
                .Logged(log, _generalSettings.EnableLogging);

            WeightedNode sendProspectorDrone = CreateSendProspectorDroneSequence(brain, allowsFollowupAction, log);
            WeightedNode identifyAsteroid = CreateIdentifyAsteroidSequence(brain, allowsFollowupAction, log);
            selector.AddChildren(new[] { sendProspectorDrone, identifyAsteroid });

            IdentifyAsteroidWeighter weighter = new IdentifyAsteroidWeighter(_aiSettings, brain);
            WeightedNode result = new WeightedNode(selectorLogged, weighter, anyUnidentifiedAsteroidsCondition);
            result.WithId(AINodeType.IdentifyAsteroid)
                .WithName("Identify asteroid motivation");

            return result;
        }

        private WeightedNode CreateSendProspectorDroneSequence(
            AIBrain brain,
            Observable<bool> allowsFollowupAction,
            Log log)
        {
            Node canPurchaseCondition = new CanPurchaseCondition(brain, _droneBuyer.CostsPerDrone)
                .WithName("Can purchase prospector drone?")
                .Logged(log, _generalSettings.EnableLogging);

            Node action = new SendProspectorDroneAction(brain, allowsFollowupAction)
                .WithName("Send prospector drone action")
                .Logged(log, _generalSettings.EnableLogging);

            Node conditions = CreateSequence(canPurchaseCondition)
                .WithName("Send prospector drone conditions")
                .Logged(log, _generalSettings.EnableLogging);

            SendProspectorDroneWeighter weighter =
                new SendProspectorDroneWeighter(brain, _sendProspectorDroneSettings);
            WeightedNode result = new WeightedNode(action, weighter, conditions);
            result.WithName("Send prospector drone")
                .WithId(AINodeType.SendProspectorDrone);
            
            return result;
        }

        private WeightedNode CreateIdentifyAsteroidSequence(AIBrain brain,
            Observable<bool> allowsFollowupAction, BasicLog log)
        {
            Node action = new FlyToAction(brain, allowsFollowupAction,
                    () => brain.GetBestProspectTargetFor(ProspectorVesselType.Ship))
                .WithName("Fly to unidentified asteroid action")
                .Logged(log, _generalSettings.EnableLogging);

            FlyToUnidentifiedAsteroidWeighter weighter =
                new FlyToUnidentifiedAsteroidWeighter(brain, _flyToUnidentifiedAsteroidAISettings);

            WeightedNode result = new WeightedNode(action, weighter);
            result.WithName("Fly to unidentified asteroid")
                .WithId(AINodeType.FlyToUnidentifiedAsteroid);
            
            return result;
        }
    }
}