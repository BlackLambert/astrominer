using SBaier.AI;
using SBaier.DI;

namespace SBaier.Astrominer
{
    public class SendCarrierDroneAIActionsFactory : AIActionsFactory
    {
        private SendCarrierDroneAISettings _sendDroneAISettings;
        
        public override void Inject(Resolver resolver)
        {
            base.Inject(resolver);
            _sendDroneAISettings = resolver.Resolve<SendCarrierDroneAISettings>();
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
            
            Node canBuyCarrierDroneCondition =
                new CanPurchaseCondition(brain, _sendDroneAISettings.CarrierDroneSettings.Price);
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

            return sendCarrierDroneWeighted;
        }
    }
}