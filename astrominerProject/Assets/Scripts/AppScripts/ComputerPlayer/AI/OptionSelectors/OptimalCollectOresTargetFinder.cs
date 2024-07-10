using System.Linq;

namespace SBaier.Astrominer
{
    public class OptimalCollectOresTargetFinder
    {
        private AiCollectOresTargetSettings _aiTargetSettings;
        private Ship _ship;
        private OreBank _oreBank;

        public OptimalCollectOresTargetFinder(
            AiCollectOresTargetSettings aiTargetSettings,
            Ship ship,
            OreBank oreBank)
        {
            _aiTargetSettings = aiTargetSettings;
            _ship = ship;
            _oreBank = oreBank;
        }
        
        public Asteroid Search()
        {
            if (_ship.Player.OwnedAsteroids.Count == 0)
            {
                return null;
            }
            
            return _ship.Player.OwnedAsteroids.Aggregate(CompareAsteroidsForCollectOres);
        }

        public float GetCollectOreValueOf(Asteroid asteroid)
        {
            if (_ship.Location.Value as Asteroid == asteroid || 
                _ship.Player.Drones.ContainsDroneTo<CarrierDrone>(asteroid))
            {
                return float.MinValue;
            }
            
            int distance = _ship.FlightMap.GetDistanceTo(asteroid);
            float distanceValue = _aiTargetSettings.DistanceFactorCurve.Evaluate(distance) * 
                                  _aiTargetSettings.DistanceValueFactor;
            float creditsToEarn = _oreBank.CalculateCreditsFor(asteroid.StoredMinedOres);
            float oreValueFactor = _aiTargetSettings.OreValueFactorCurve.Evaluate(creditsToEarn);
            float oreValueWeight = oreValueFactor * 
                                   _aiTargetSettings.ValueFactor;
            return distanceValue + oreValueWeight;
        }

        private Asteroid CompareAsteroidsForCollectOres(Asteroid asteroid1, Asteroid asteroid2)
        {
            float value1 = GetCollectOreValueOf(asteroid1);
            float value2 = GetCollectOreValueOf(asteroid2);
            return value1 > value2 ? asteroid1 : asteroid2;
        }
    }
}