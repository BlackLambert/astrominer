using System.Collections.Generic;
using System.Linq;

namespace SBaier.Astrominer
{
    public class OptimalProspectTargetFinder
    {
        private readonly Map _map;
        private readonly Ship _ship;
        private readonly AIProspectingSettings _prospectingSettings;

        public OptimalProspectTargetFinder(
            Map map,
            Ship ship,
            AIProspectingSettings prospectingSettings)
        {
            _map = map;
            _ship = ship;
            _prospectingSettings = prospectingSettings;
        }
        
        public Asteroid Search()
        {
            List<Asteroid> unidentifiedAsteroids = _map.GetUnidentifiedAsteroids(_ship.Player);
            
            if (unidentifiedAsteroids.Count == 0)
            {
                return null;
            }
            
            return unidentifiedAsteroids.Aggregate(CompareAsteroidsForProspecting);
        }

        public float GetProspectingValueOf(Asteroid asteroid)
        {
            int distance = _ship.FlightMap.GetDistanceTo(asteroid);
            float distanceValue = _prospectingSettings.DistanceFactorCurve.Evaluate(distance) * 
                                  _prospectingSettings.DistanceValueFactor;
            float sizeValue = asteroid.Size * _prospectingSettings.SizeValueFactor;
            return distanceValue + sizeValue;
        }

        private Asteroid CompareAsteroidsForProspecting(Asteroid asteroid1, Asteroid asteroid2)
        {
            float value1 = GetProspectingValueOf(asteroid1);
            float value2 = GetProspectingValueOf(asteroid2);
            return value1 > value2 ? asteroid1 : asteroid2;
        }
    }
}