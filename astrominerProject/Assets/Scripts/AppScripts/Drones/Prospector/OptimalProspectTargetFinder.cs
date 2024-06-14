using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class OptimalProspectTargetFinder
    {
        private readonly Map _map;
        private readonly Player _player;
        private readonly float _asteroidDistanceMaxWeightValue;
        private readonly float _distanceVertex;
        private readonly float _distanceVariance;

        public OptimalProspectTargetFinder(
            Map map,
            Player player,
            Vector2 idealDistanceRange,
            float asteroidDistanceMaxWeightValue)
        {
            
            _map = map;
            _player = player;
            _asteroidDistanceMaxWeightValue = asteroidDistanceMaxWeightValue;
            float halfVariance = (idealDistanceRange.y - idealDistanceRange.x) / 2;
            _distanceVertex = idealDistanceRange.x + halfVariance;
            _distanceVariance = -_asteroidDistanceMaxWeightValue / (_distanceVertex * _distanceVertex);
        }
        
        public Asteroid GetBestUnidentifiedAsteroid(FlyTarget location)
        {
            List<Asteroid> unidentifiedAsteroids = _map.GetUnidentifiedAsteroids(_player);
            
            if (unidentifiedAsteroids.Count == 0)
            {
                return null;
            }
            
            return unidentifiedAsteroids.Aggregate(
                (asteroid1, asteroid2) => CompareAsteroids(location, asteroid1, asteroid2));
        }

        public float GetProspectingValueOf(Asteroid asteroid, FlyTarget target)
        {
            float distance = asteroid.DistanceTo(target);
            float xMinusD = distance - _distanceVertex;
            return _distanceVariance * xMinusD * xMinusD + _asteroidDistanceMaxWeightValue;
        }

        private Asteroid CompareAsteroids(FlyTarget location, Asteroid asteroid1, Asteroid asteroid2)
        {
            float value1 = GetProspectingValueOf(asteroid1, location);
            float value2 = GetProspectingValueOf(asteroid2, location);
            return value1 > value2 ? asteroid1 : asteroid2;
        }
    }
}