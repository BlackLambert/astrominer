using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class ActiveShipsEnqueuer : MonoBehaviour, Injectable
    {
        private Ship _ship;
        private QueuedShips _queuedShips;
        private ActiveItem<Ship> _activeShip;

        public void Inject(Resolver resolver)
        {
            _ship = resolver.Resolve<Ship>();
            _queuedShips = resolver.Resolve<QueuedShips>();
            _activeShip = resolver.Resolve<ActiveItem<Ship>>();
        }

        private void OnEnable()
        {
            _ship.OnFlyTargetChanged += OnTargetChanged;
            _ship.OnFlyTargetReached += OnTargetReached;
        }

        private void OnDisable()
        {
            _ship.OnFlyTargetChanged -= OnTargetChanged;
            _ship.OnFlyTargetReached -= OnTargetReached;
        }

        private void OnTargetReached()
        {
            _queuedShips.Enqueue(_ship);
        }

        private void OnTargetChanged()
        {
            _activeShip.Value = null;
        }
    }
}
