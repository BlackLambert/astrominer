using System;
using SBaier.AI;
using SBaier.DI;

namespace SBaier.Astrominer
{
    public class IdentifyAsteroidAIActionsFactory : AIActionsFactory, Injectable
    {
        private IdentifyAsteroidAISettings _aiSettings;
        private SendProspectorDroneAISettings _sendProspectorDroneSettings;
        private FlyToUnidentifiedAsteroidAISettings _flyToUnidentifiedAsteroidAISettings;
        private DroneBuyer<ProspectorDrone> _droneBuyer;
        
        public void Inject(Resolver resolver)
        {
            _sendProspectorDroneSettings = resolver.Resolve<SendProspectorDroneAISettings>();
            _droneBuyer = resolver.Resolve<DroneBuyer<ProspectorDrone>>();
            _aiSettings = resolver.Resolve<IdentifyAsteroidAISettings>();
            _flyToUnidentifiedAsteroidAISettings = resolver.Resolve<FlyToUnidentifiedAsteroidAISettings>();
        }
        
        public WeightedNode Create(
            AIBrain brain,
            Observable<bool> allowsFollowupAction)
        {
            Node anyUnidentifiedAsteroidsCondition = new Condition(() => brain.HasUnidentifiedAsteroid)
                .WithName("Is there any unidentified asteroid?");

            WeightedSelector selector = new WeightedSelector();
            selector.WithId(AINodeType.IdentifyAsteroid).WithName("Identify asteroid selector");

            WeightedNode sendProspectorDrone = CreateSendProspectorDroneSequence(brain, allowsFollowupAction);
            WeightedNode identifyAsteroid = CreateIdentifyAsteroidSequence(brain, allowsFollowupAction);
            selector.AddChildren(new[] {sendProspectorDrone, identifyAsteroid});

            IdentifyAsteroidWeighter weighter = new IdentifyAsteroidWeighter(_aiSettings, brain);
            WeightedNode result = new WeightedNode(selector, weighter, anyUnidentifiedAsteroidsCondition);
            result.WithId(AINodeType.IdentifyAsteroid).WithName("Identify asteroid motivation");
            return result;
        }
        
        private WeightedNode CreateSendProspectorDroneSequence(
            AIBrain brain,
            Observable<bool> allowsFollowupAction)
        {
            Node canPurchaseCondition =
                new CanPurchaseCondition(brain, _droneBuyer.CostsPerDrone).WithName("Can purchase prospector drone?");

            Node action = new SendProspectorDroneAction(brain, allowsFollowupAction)
                .WithName("Send prospector drone action");
            
            Node conditions = CreateSequence(canPurchaseCondition)
                .WithName("Send prospector drone conditions");

            SendProspectorDroneWeighter weighter =
                new SendProspectorDroneWeighter(brain, _sendProspectorDroneSettings);
            WeightedNode result = new WeightedNode(action, weighter, conditions);
            result.WithName("Send prospector drone").WithId(AINodeType.SendProspectorDrone);
            return result;
        }

        private WeightedNode CreateIdentifyAsteroidSequence(
            AIBrain brain,
            Observable<bool> allowsFollowupAction)
        {
            Node action = new FlyToAction(brain, allowsFollowupAction, 
                    () => brain.GetBestProspectTargetFor(ProspectorVesselType.Ship))
                .WithName("Fly to unidentified asteroid action");

            FlyToUnidentifiedAsteroidWeighter weighter = 
                new FlyToUnidentifiedAsteroidWeighter(brain, _flyToUnidentifiedAsteroidAISettings);
            
            WeightedNode result = new WeightedNode(action, weighter);
            result.WithName("Fly to unidentified asteroid").WithId(AINodeType.SendProspectorDrone);
            return result;
        }
    }
}