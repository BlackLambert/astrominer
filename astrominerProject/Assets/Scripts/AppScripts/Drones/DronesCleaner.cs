using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class DronesCleaner : MonoBehaviour, Injectable, Cleanable
    {
        private Players _players;
        private Pool<CarrierDrone, DroneArguments> _carrierDronePool;
        private Pool<ProspectorDrone, DroneArguments> _prospectorDronePool;
        
        public void Inject(Resolver resolver)
        {
            _players = resolver.Resolve<Players>();
            _carrierDronePool = resolver.Resolve<Pool<CarrierDrone, DroneArguments>>();
            _prospectorDronePool = resolver.Resolve<Pool<ProspectorDrone, DroneArguments>>();
        }

        public void Clean()
        {
            foreach (Player player in _players)
            {
                foreach (Drone drone in player.Drones)
                {
                    Pool(drone);
                }
            }
        }

        private void Pool(Drone drone)
        {
            switch (drone)
            {
                case CarrierDrone carrierDrone:
                    _carrierDronePool.Return(carrierDrone);
                    break;
                case ProspectorDrone prospectorDrone:
                    _prospectorDronePool.Return(prospectorDrone);
                    break;
            }
        }
    }
}
