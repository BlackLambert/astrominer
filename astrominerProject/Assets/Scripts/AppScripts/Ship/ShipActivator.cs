using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class ShipActivator : MonoBehaviour, Injectable
    {
        private QueuedShips _queuedShips;
        private ActiveItem<Ship> _activeShip;
        private ActiveItem<Player> _activePlayer;

        public void Inject(Resolver resolver)
        {
            _queuedShips = resolver.Resolve<QueuedShips>();
            _activeShip = resolver.Resolve<ActiveItem<Ship>>();
            _activePlayer = resolver.Resolve<ActiveItem<Player>>();
        }

        private void OnEnable()
        {
            TryActivateNextShip();
            _queuedShips.OnEnqueued += OnEnqueued;
            _activeShip.OnValueChanged += OnActiveShipChanged;
        }

        private void OnDisable()
        {
            _queuedShips.OnEnqueued -= OnEnqueued;
            _activeShip.OnValueChanged -= OnActiveShipChanged;
        }


        private void OnEnqueued(Ship ship)
        {
            TryActivateNextShip();
        }

        private void OnActiveShipChanged()
        {
            if (!_activeShip.HasValue)
            {
                _activePlayer.Value = null;
                Time.timeScale = 1;
                TryActivateNextShip();
            }
        }

        private void TryActivateNextShip()
        {
            if (!_queuedShips.HasNext() || _activeShip.HasValue)
            {
                return;
            }

            Ship next = _queuedShips.Dequeue();
            _activeShip.Value = next;
            _activePlayer.Value = next.Player;
            Time.timeScale = 0;
        }
    }
}
