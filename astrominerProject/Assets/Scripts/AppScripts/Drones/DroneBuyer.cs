using System;
using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class DroneBuyer<TDrone> : Injectable where TDrone : Drone
    {
        public float CostsPerDrone => _settings.Price;
        private Pool<TDrone, DroneArguments, PrefabInstantiationArguments> _pool;
        private DroneSettings _settings;
        private Transform _hook;

        public void Inject(Resolver resolver)
        {
            _pool = resolver.Resolve<Pool<TDrone, DroneArguments, PrefabInstantiationArguments>>();
            _settings = resolver.Resolve<DroneSettings>();
            _hook = resolver.Resolve<Transform>("DronesHook");
        }

        public void BuyDrone(Ship ship, Asteroid asteroid, Base playerBase)
        {
            if (ship.Player.Credits.Amount < _settings.Price)
            {
                throw new InvalidOperationException($"Player {ship.Player.Name} needs more credits to buy the drone");
            }

            ship.Player.Credits.Request(_settings.Price);
            DroneArguments settings = new DroneArguments(ship.Location.Value, asteroid, playerBase, ship.Player);
            TDrone drone = _pool.Request(settings,
                new PrefabInstantiationArguments() { Position = ship.Position2D, Parent = _hook});
            ship.Player.Drones.Add(drone);
        }
    }
}