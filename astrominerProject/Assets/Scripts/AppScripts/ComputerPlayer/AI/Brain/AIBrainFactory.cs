using SBaier.DI;

namespace SBaier.Astrominer
{
    public class AIBrainFactory : Factory<AIBrain, Ship>, Injectable
    {
        private Resolver _resolver;
        private Bases _bases;
        private SendProspectorDroneAISettings _sendProspectorDroneAISettings;
        private FlyToUnidentifiedAsteroidAISettings _flyToUnidentifiedAsteroidAISettings;
        private OccupyAsteroidAiSettings _occupyAsteroidAiSettings;
        private BuyExploiterAISettings _buyExploiterAISettings;
        private ExploitMachineSettings _exploitMachineSettings;
        private SendCarrierDroneAISettings _sendCarrierDroneAISettings;
        private CollectOresAISettings _collectOresAISettings;
        private TakeExploiterAISettings _takeExploiterAISettings;
        private SellOresAISettings _sellOresAISettings;
        private Map _map;
        private OreBank _oreBank;


        public void Inject(Resolver resolver)
        {
            _resolver = resolver;
            _bases = resolver.Resolve<Bases>();
            _sendProspectorDroneAISettings = resolver.Resolve<SendProspectorDroneAISettings>();
            _flyToUnidentifiedAsteroidAISettings = resolver.Resolve<FlyToUnidentifiedAsteroidAISettings>();
            _occupyAsteroidAiSettings = resolver.Resolve<OccupyAsteroidAiSettings>();
            _buyExploiterAISettings = resolver.Resolve<BuyExploiterAISettings>();
            _exploitMachineSettings = resolver.Resolve<ExploitMachineSettings>();
            _sendCarrierDroneAISettings = resolver.Resolve<SendCarrierDroneAISettings>();
            _collectOresAISettings = resolver.Resolve<CollectOresAISettings>();
            _takeExploiterAISettings = resolver.Resolve<TakeExploiterAISettings>();
            _sellOresAISettings = resolver.Resolve<SellOresAISettings>();
            _oreBank = resolver.Resolve<OreBank>();
            _map = resolver.Resolve<Map>();
        }

        public AIBrain Create(Ship ship)
        {
            AIBrain result = new AIBrain();
            ArgumentsResolver resolver = new ArgumentsResolver(_resolver);
            resolver.AddArgument(ship);
            resolver.AddArgument(_bases.Get(ship.Player));
            resolver.AddArgument(
                CreateProspectTargetFinder(ship, _sendProspectorDroneAISettings.ProspectingSettings),
                ProspectorVesselType.Drone);
            resolver.AddArgument(
                CreateProspectTargetFinder(ship, _flyToUnidentifiedAsteroidAISettings.ProspectingSettings),
                ProspectorVesselType.Ship);
            resolver.AddArgument(
                CreateCollectOresTargetFinder(ship, _sendCarrierDroneAISettings.CollectOresTargetSettings),
                CollectOresVesselType.Drone);
            resolver.AddArgument(
                CreateCollectOresTargetFinder(ship, _collectOresAISettings.CollectOresTargetSettings),
                CollectOresVesselType.Ship);
            resolver.AddArgument(
                new OptimalExploitTargetFinder(ship, _occupyAsteroidAiSettings.ExploitTargetSettings));
            resolver.AddArgument(
                new OptimalExploitersToBuyFinder(ship, _buyExploiterAISettings, _exploitMachineSettings));
            resolver.AddArgument(
                new OptimalTakeExploiterTargetFinder(_takeExploiterAISettings.TakeExploiterTargetSettings, ship));
            resolver.AddArgument(
                new OptimalOresToSellFinder(ship, _oreBank, _sellOresAISettings.OresToSellFinderSettings));
            result.Inject(resolver);
            return result;
        }

        private OptimalProspectTargetFinder CreateProspectTargetFinder(Ship ship, AIProspectingSettings settings)
        {
            return new OptimalProspectTargetFinder(
                _map,
                ship,
                settings);
        }

        private OptimalCollectOresTargetFinder CreateCollectOresTargetFinder(Ship ship, AiCollectOresTargetSettings settings)
        {
            return new OptimalCollectOresTargetFinder(
                settings,
                ship,
                _oreBank);
        }
    }
}