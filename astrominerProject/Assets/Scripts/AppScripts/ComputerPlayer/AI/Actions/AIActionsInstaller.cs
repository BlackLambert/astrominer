using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class AIActionsInstaller : MonoInstaller
    {
        [SerializeField] 
        private BuyExploiterActionSettings _buyExploiterSettings;
        [SerializeField]
        private SendProspectorDroneActionSettings _sendProspectorDroneSettings;
        [SerializeField] 
        private PlaceExploiterActionSettings _placeExploiterActionSettings;
        
        public override void InstallBindings(Binder binder)
        {
            binder.Bind<Factory<AgentActor, Ship>>()
                .ToNew<AgentActorFactory>();

            binder.BindInstance(_buyExploiterSettings);
            binder.BindInstance(_sendProspectorDroneSettings);
            binder.BindInstance(_placeExploiterActionSettings);
        }
    }
}
