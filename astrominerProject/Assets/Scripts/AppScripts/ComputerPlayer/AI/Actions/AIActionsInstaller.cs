using System.Collections.Generic;
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
        
        public override void InstallBindings(Binder binder)
        {
            binder.Bind<Factory<List<AIAction>>>()
                .ToNew<AIActionsFactory>();
            
            binder.Bind<Factory<FlyToRandomAsteroidAction>>()
                .ToNew<BasicFactory<FlyToRandomAsteroidAction>>();
            
            binder.Bind<Factory<BuyExploiterAction, BuyExploiterActionSettings>>()
                .ToNew<BasicFactory<BuyExploiterAction, BuyExploiterActionSettings>>();
            
            binder.Bind<Factory<SendProspectorDroneAction, SendProspectorDroneActionSettings>>()
                .ToNew<BasicFactory<SendProspectorDroneAction, SendProspectorDroneActionSettings>>();

            binder.BindInstance(_buyExploiterSettings);
            binder.BindInstance(_sendProspectorDroneSettings);
        }
    }
}
