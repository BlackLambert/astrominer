using System.Linq;
using SBaier.AI;

namespace SBaier.Astrominer
{
    public class SendProspectorDroneWeighter : Weighter
    {
        private readonly Ship _ship;
        private readonly SendProspectorDroneActionSettings _settings;
        private readonly OptimalProspectTargetFinder _prospectTargetFinder;

        public SendProspectorDroneWeighter(
            Observable<Weight> weight,
            Map map,
            Ship ship,
            SendProspectorDroneActionSettings settings) : base(weight)
        {
            _ship = ship;
            _settings = settings;
            _prospectTargetFinder = new OptimalProspectTargetFinder(map, _ship.Player, settings.IdealDistanceRange,
                settings.AsteroidDistanceMaxWeightValue);
        }

        protected override float GetWeight()
        {
            Player player = _ship.Player;
            
            Asteroid mostValuableUnidentifiedAsteroid = _prospectTargetFinder.GetBestUnidentifiedAsteroid(_ship.Location.Value);
            
            //Any interesting asteroid?
            if (mostValuableUnidentifiedAsteroid == null)
            {
                return float.MinValue;
            }

            float weight = _settings.BaseWeight;
            
            // Any identified empty asteroids?
            float emptyIdentifiedAsteroidsValueSum = player.IdentifiedAsteroids.Where(asteroid => !asteroid.HasExploitMachine)
                .Sum(asteroid => asteroid.Value);
            
            if (emptyIdentifiedAsteroidsValueSum <= 0)
            {
                weight += _settings.NoEmptyIdentifiedAsteroidsWeightValue;
            }
            else
            {
                weight += _settings.IdentifiedAsteroidsValueWeightFactor * emptyIdentifiedAsteroidsValueSum;
            }
            
            // Mining asteroids value
            float miningAsteroidsValueSum = player.IdentifiedAsteroids
                .Where(asteroid => asteroid.OwningPlayer == player)
                .Sum(asteroid => asteroid.Value);
            weight += miningAsteroidsValueSum * _settings.MiningAsteroidsValueWeightFactor;

            // Active drones amount
            int dronesAmount = player.Drones.Count;
            weight += dronesAmount * _settings.ActiveDronesWeightReductionFactor;

            // Interesting asteroids far enough?
            float asteroidProspectingValue = _prospectTargetFinder.GetProspectingValueOf(
                mostValuableUnidentifiedAsteroid, _ship.Location.Value);
            weight += asteroidProspectingValue;

            return weight;
        }
    }
}