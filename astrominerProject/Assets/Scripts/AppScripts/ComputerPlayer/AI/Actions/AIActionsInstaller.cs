using SBaier.DI;
using UnityEngine;

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
        
        public override void InstallBindings(Binder binder)
        {
            binder.Bind<Factory<AIBrain, Ship>>()
                .ToNew<AIBrainFactory>();
            
            binder.BindToNewSelf<IdentifyAsteroidAIActionsFactory>();
            binder.BindToNewSelf<OccupyAsteroidAIActionsFactory>();
            
            binder.Bind<Factory<AgentActor, Ship>>()
                .ToNew<AgentActorFactory>();
            
            binder.BindInstance(_identifyAsteroidAISettings);
            binder.BindInstance(_sendProspectorDroneAISettings);
            binder.BindInstance(_flyToUnidentifiedAsteroidAISettings);
            binder.BindInstance(_occupyAsteroidAiSettings);
            binder.BindInstance(_buyExploiterAISettings);
        }
    }
}
