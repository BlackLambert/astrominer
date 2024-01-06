using System;
using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class ProspectorDroneBuyer : Injectable
    {
        private Factory<ProspectorDrone, DroneArguments> _factory;
        private ProspectorDroneSettings _settings;

        public void Inject(Resolver resolver)
        {
            _factory = resolver.Resolve<Factory<ProspectorDrone, DroneArguments>>();
            _settings = resolver.Resolve<ProspectorDroneSettings>();
        }
        
        public ProspectorDrone BuyDrone(Ship ship, Asteroid asteroid, Base playerBase)
        {
            if (ship.Player.Credits.Amount < _settings.Price)
            {
                throw new InvalidOperationException($"Player {ship.Player.Name} needs more credits to buy a prospector drone");
            }

            ship.Player.Credits.Request(_settings.Price);
            Vector2 startPosition = ship.Position2D;
            DroneArguments settings = new DroneArguments(startPosition, asteroid, playerBase, ship.Player);
            ProspectorDrone drone = _factory.Create(settings);
            drone.transform.position = startPosition;
            return drone;
        }
    }
}