using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class ActiveShipsEnqueuer : MonoBehaviour, Injectable
    {
        private Ship _ship;
        private QueuedShips _queuedShips;

        public void Inject(Resolver resolver)
        {
            _ship = resolver.Resolve<Ship>();
            _queuedShips = resolver.Resolve<QueuedShips>();
        }

        private void OnEnable()
        {
            _ship.OnLocationChanged += OnLocationChanged;
        }

        private void OnDisable()
        {
            _ship.OnLocationChanged -= OnLocationChanged;
        }

        private void OnLocationChanged()
        {
            if (_ship.Location != null)
            {
                _queuedShips.Enqueue(_ship);
            }
        }
    }
}
