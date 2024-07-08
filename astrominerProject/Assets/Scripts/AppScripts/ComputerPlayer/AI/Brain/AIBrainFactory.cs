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
        private Map _map;


        public void Inject(Resolver resolver)
        {
            _resolver = resolver;
            _bases = resolver.Resolve<Bases>();
            _sendProspectorDroneAISettings = resolver.Resolve<SendProspectorDroneAISettings>();
            _flyToUnidentifiedAsteroidAISettings = resolver.Resolve<FlyToUnidentifiedAsteroidAISettings>();
            _occupyAsteroidAiSettings = resolver.Resolve<OccupyAsteroidAiSettings>();
            _buyExploiterAISettings = resolver.Resolve<BuyExploiterAISettings>();
            _exploitMachineSettings = resolver.Resolve<ExploitMachineSettings>();
            _map = resolver.Resolve<Map>();
        }

        public AIBrain Create(Ship ship)
        {
            AIBrain result = new AIBrain();
            ArgumentsResolver resolver = new ArgumentsResolver(_resolver);
            OptimalExploitTargetFinder exploitTargetFinder =
                new OptimalExploitTargetFinder(ship, _occupyAsteroidAiSettings.ExploitTargetSettings);
            resolver.AddArgument(ship);
            resolver.AddArgument(_bases.Get(ship.Player));
            resolver.AddArgument(
                CreateProspectTargetFinder(ship, _sendProspectorDroneAISettings.ProspectingSettings),
                ProspectorVesselType.Drone);
            resolver.AddArgument(
                CreateProspectTargetFinder(ship, _flyToUnidentifiedAsteroidAISettings.ProspectingSettings),
                ProspectorVesselType.Ship);
            resolver.AddArgument(exploitTargetFinder);
            resolver.AddArgument(
                new OptimalExploitersToBuyFinder(ship, _buyExploiterAISettings, _exploitMachineSettings));
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
    }
}