using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class DronePooler<TDrone> : MonoBehaviour, Injectable, Cleanable, Initializable where TDrone : Drone
    {
        private Player _player;
        private TDrone _drone;
        private Pool<TDrone, DroneArguments> _pool;
        
        public void Inject(Resolver resolver)
        {
            _player = resolver.Resolve<Player>();
            _drone = resolver.Resolve<TDrone>();
            _pool = resolver.Resolve<Pool<TDrone, DroneArguments>>();
        }

        public void Initialize()
        {
            _drone.OnDone += PoolDrone;
        }

        public void Clean()
        {
            if (_player.Drones.Contains(_drone))
            {
                PoolDrone();
            }
        }

        private void PoolDrone()
        {
            _drone.OnDone -= PoolDrone;
            _player.Drones.Remove(_drone);
            _pool.Return(_drone);
        }
    }
}
