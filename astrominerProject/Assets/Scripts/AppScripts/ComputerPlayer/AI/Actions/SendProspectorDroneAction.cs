using SBaier.AI;

namespace SBaier.Astrominer
{
    public class SendProspectorDroneAction : Node
    {
        private readonly SendProspectorDroneActionSettings _settings;
        private readonly DroneBuyer<ProspectorDrone> _buyer;
        private readonly Ship _ship;
        private readonly Map _map;
        private readonly Bases _bases;
        private readonly Observable<bool> _allowsFollowupAction;
        private readonly OptimalProspectTargetFinder _prospectTargetFinder;
        
        public SendProspectorDroneAction(
            SendProspectorDroneActionSettings settings,
            DroneBuyer<ProspectorDrone> buyer,
            Ship ship,
            Map map,
            Bases bases,
            Observable<bool> allowsFollowupAction
            )
        {
            _settings = settings;
            _buyer = buyer;
            _ship = ship;
            _map = map;
            _bases = bases;
            _allowsFollowupAction = allowsFollowupAction;
            _prospectTargetFinder = new OptimalProspectTargetFinder(_map, _ship.Player, _settings.IdealDistanceRange,
                _settings.AsteroidDistanceMaxWeightValue);
        }

        public override bool Execute()
        {
            Asteroid asteroid = _prospectTargetFinder.GetBestUnidentifiedAsteroid(_ship.Location.Value);
            _ship.Player.Drones.Add(_buyer.BuyDrone(_ship, asteroid, _bases.Get(_ship.Player)));
            _allowsFollowupAction.Value = true;
            return true;
        }
    }
}