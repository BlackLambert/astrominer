using SBaier.DI;
using UnityEngine;
using UnityEngine.Serialization;

namespace SBaier.Astrominer
{
    public class AIActionsInstaller : MonoInstaller
    {
        [SerializeField] 
        private IdentifyAsteroidAISettings _identifyAsteroidAISettings;

        [SerializeField] 
        private SendProspectorDroneAISettings _sendProspectorDroneAISettings;

        [SerializeField] 
        private FlyToUnidentifiedAsteroidAISettings _flyToUnidentifiedAsteroidAISettings;

        [SerializeField] 
        private OccupyAsteroidAiSettings _occupyAsteroidAiSettings;

        [SerializeField] 
        private BuyExploiterAISettings _buyExploiterAISettings;
        
        [SerializeField] 
        private SendCarrierDroneAISettings _sendCarrierDroneAISettings;
        
        [SerializeField] 
        private CollectOresAISettings _collectOresAISettings;
        
        [SerializeField] 
        private SellOresAISettings _sellOresAISettings;

        [SerializeField] 
        private EarnCreditsAiSettings _earnCreditsAiSettings;
        
        public override void InstallBindings(Binder binder)
        {
            binder.Bind<Factory<AIBrain, Ship>>()
                .ToNew<AIBrainFactory>();
            
            binder.BindToNewSelf<IdentifyAsteroidAIActionsFactory>();
            binder.BindToNewSelf<OccupyAsteroidAIActionsFactory>();
            binder.BindToNewSelf<EarnCreditsAIActionsFactory>();
            
            binder.Bind<Factory<AgentActor, Ship>>()
                .ToNew<AgentActorFactory>();
            
            binder.BindInstance(_identifyAsteroidAISettings);
            binder.BindInstance(_sendProspectorDroneAISettings);
            binder.BindInstance(_flyToUnidentifiedAsteroidAISettings);
            binder.BindInstance(_occupyAsteroidAiSettings);
            binder.BindInstance(_buyExploiterAISettings);
            binder.BindInstance(_sendCarrierDroneAISettings);
            binder.BindInstance(_collectOresAISettings);
            binder.BindInstance(_sellOresAISettings);
            binder.BindInstance(_earnCreditsAiSettings);
        }
    }
}
