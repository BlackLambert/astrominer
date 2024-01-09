using System;
using SBaier.DI;

namespace SBaier.Astrominer
{
    public class CarrierDroneBuyer : Injectable
    {
        private Factory<CarrierDrone, DroneArguments> _factory;
        private DroneSettings _settings;

        public void Inject(Resolver resolver)
        {
            _factory = resolver.Resolve<Factory<CarrierDrone, DroneArguments>>();
            _settings = resolver.Resolve<DroneSettings>();
        }
        
        public Drone BuyDrone(Ship ship, Asteroid asteroid, Base playerBase)
        {
            if (ship.Player.Credits.Amount < _settings.Price)
            {
                throw new InvalidOperationException($"Player {ship.Player.Name} needs more credits to buy a prospector drone");
            }

            ship.Player.Credits.Request(_settings.Price);
            DroneArguments settings = new DroneArguments(ship.Location.Value, asteroid, playerBase, ship.Player);
            CarrierDrone drone = _factory.Create(settings);
            drone.transform.position = ship.Position2D;
            return drone;
        }
    }
}
