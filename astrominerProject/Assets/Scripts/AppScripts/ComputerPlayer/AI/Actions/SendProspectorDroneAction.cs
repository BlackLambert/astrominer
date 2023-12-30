using System.Collections.Generic;
using System.Linq;
using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class SendProspectorDroneAction : AIAction, Injectable
    {
        public bool AllowsFollowAction => true;
        private SendProspectorDroneActionSettings _settings;
        private ProspectorDroneBuyer _buyer;
        private ProspectorDroneSettings _droneSettings;
        private Map _map;
        private Bases _bases;

        private float _distanceVariance;
        private float _distanceVertex;

        public void Inject(Resolver resolver)
        {
            _settings = resolver.Resolve<SendProspectorDroneActionSettings>();
            _droneSettings = resolver.Resolve<ProspectorDroneSettings>();
            _buyer = resolver.Resolve<ProspectorDroneBuyer>();
            _map = resolver.Resolve<Map>();
            _bases = resolver.Resolve<Bases>();

            float halfVariance = (_settings.IdealDistanceRange.y - _settings.IdealDistanceRange.x) / 2;
            _distanceVertex = _settings.IdealDistanceRange.x + halfVariance;
            _distanceVariance = -_settings.AsteroidDistanceMaxWeightValue / (_distanceVertex * _distanceVertex);
        }
        
        public float GetCurrentWeight(Ship ship)
        {
            Player player = ship.Player;
            
            // Enough money?
            if (!player.Credits.Has(_droneSettings.Price))
            {
                return float.MinValue;
            }
            
            Asteroid mostValuableUnidentifiedAsteroid = GetBestUnidentifiedAsteroid(ship);
            
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
            int dronesAmount = player.ProspectorDrones.Count;
            weight += dronesAmount * _settings.ActiveDronesWeightReductionFactor;

            // Interesting asteroids far enough?
            float asteroidProspectingValue = GetProspectingValueOf(mostValuableUnidentifiedAsteroid, ship.Location.Value);
            Debug.Log($"Asteroid: {mostValuableUnidentifiedAsteroid.name} | Prospecting value: {asteroidProspectingValue}");
            weight += asteroidProspectingValue;

            return weight;
        }

        public void Execute(Ship ship)
        {
            Asteroid asteroid = GetBestUnidentifiedAsteroid(ship);
            ship.Player.ProspectorDrones.Add(_buyer.BuyDrone(ship, asteroid, _bases.Get(ship.Player)));
        }

        private Asteroid GetBestUnidentifiedAsteroid(Ship ship)
        {
            Player player = ship.Player;
            
            List<Asteroid> unidentifiedAsteroids = _map.Asteroids.Value.Where(
                asteroid => IsUnidentifiedAsteroid(asteroid, ship, player)).ToList();
            
            if (unidentifiedAsteroids.Count == 0)
            {
                return null;
            }
            
            return unidentifiedAsteroids.Aggregate(
                (asteroid1, asteroid2) => CompareAsteroids(ship, asteroid1, asteroid2));
        }

        private bool IsUnidentifiedAsteroid(Asteroid asteroid, Ship ship, Player player)
        {
            return !asteroid.HasOwningPlayer &&
                   !player.IdentifiedAsteroids.Contains(asteroid) &&
                   ship.Player.ProspectorDrones.All(drone => drone.Target != asteroid);
        }

        private Asteroid CompareAsteroids(Ship ship, Asteroid asteroid1, Asteroid asteroid2)
        {
            FlyTarget target = ship.Location.Value;
            float value1 = GetProspectingValueOf(asteroid1, target);
            float value2 = GetProspectingValueOf(asteroid2, target);
            return value1 > value2 ? asteroid1 : asteroid2;
        }

        private float GetProspectingValueOf(Asteroid asteroid, FlyTarget target)
        {
            float distance = asteroid.DistanceTo(target);
            float xMinusD = distance - _distanceVertex;
            return _distanceVariance * xMinusD * xMinusD + _settings.AsteroidDistanceMaxWeightValue;
        }
    }
}
