using System.Collections.Generic;
using SBaier.DI;

namespace SBaier.Astrominer
{
    public class AIActionsFactory : Factory<List<AIAction>>, Injectable
    {
        private Factory<FlyToRandomAsteroidAction> _randomAsteroidActionFactory;
        private Factory<BuyExploiterAction, BuyExploiterActionSettings> _buyExploiterActionFactory;
        private Factory<SendProspectorDroneAction, SendProspectorDroneActionSettings> _sendProspectorDroneActionFactory;
        
        private BuyExploiterActionSettings _buyExploiterSettings;
        private SendProspectorDroneActionSettings _sendProspectorDroneSettings;
        
        public void Inject(Resolver resolver)
        {
            _randomAsteroidActionFactory = resolver.Resolve<Factory<FlyToRandomAsteroidAction>>();
            _buyExploiterActionFactory = resolver.Resolve<Factory<BuyExploiterAction, BuyExploiterActionSettings>>();
            _sendProspectorDroneActionFactory =
                resolver.Resolve<Factory<SendProspectorDroneAction, SendProspectorDroneActionSettings>>();

            _buyExploiterSettings = resolver.Resolve<BuyExploiterActionSettings>();
            _sendProspectorDroneSettings = resolver.Resolve<SendProspectorDroneActionSettings>();
        }

        public List<AIAction> Create()
        {
            List<AIAction> result = new List<AIAction>();
            result.Add(_randomAsteroidActionFactory.Create());
            result.Add(_buyExploiterActionFactory.Create(_buyExploiterSettings));
            result.Add(_sendProspectorDroneActionFactory.Create(_sendProspectorDroneSettings));
            return result;
        }
    }
}
