using System;
using SBaier.DI;

namespace SBaier.Astrominer
{
    public class DroneBuyer<TDrone> : Injectable where TDrone : Drone
    {
        public float CostsPerDrone => _settings.Price;
        private Pool<TDrone, DroneArguments, PrefabInstantiationArguments> _pool;
        private DroneSettings _settings;

        public void Inject(Resolver resolver)
        {
            _pool = resolver.Resolve<Pool<TDrone, DroneArguments, PrefabInstantiationArguments>>();
            _settings = resolver.Resolve<DroneSettings>();
        }

        public Drone BuyDrone(Ship ship, Asteroid asteroid, Base playerBase)
        {
            if (ship.Player.Credits.Amount < _settings.Price)
            {
                throw new InvalidOperationException($"Player {ship.Player.Name} needs more credits to buy the drone");
            }

            ship.Player.Credits.Request(_settings.Price);
            DroneArguments settings = new DroneArguments(ship.Location.Value, asteroid, playerBase, ship.Player);
            TDrone drone = _pool.Request(settings,
                new PrefabInstantiationArguments() { Position = ship.Position2D });
            drone.OnDone += OnDroneDone;
            return drone;
        }

        private void OnDroneDone(Drone drone)
        {
            drone.OnDone -= OnDroneDone;
            _pool.Return(drone as TDrone);
        }
    }
}