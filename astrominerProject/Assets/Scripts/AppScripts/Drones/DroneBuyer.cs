using System;
using SBaier.DI;

namespace SBaier.Astrominer
{
    public class DroneBuyer<TDrone> : Injectable where TDrone : Drone
    {
        private Factory<TDrone, DroneArguments> _factory;
        private DroneSettings _settings;

        public void Inject(Resolver resolver)
        {
            _factory = resolver.Resolve<Factory<TDrone, DroneArguments>>();
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
            TDrone drone = _factory.Create(settings);
            drone.transform.position = ship.Position2D;
            return drone;
        }
    }
}
