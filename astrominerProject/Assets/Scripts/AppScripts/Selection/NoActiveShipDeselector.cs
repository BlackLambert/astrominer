using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class NoActiveShipDeselector : MonoBehaviour, Injectable
    {
        private Selection _selection;
        private ActiveItem<Ship> _activeShip;

        public void Inject(Resolver resolver)
        {
            _selection = resolver.Resolve<Selection>();
            _activeShip = resolver.Resolve<ActiveItem<Ship>>();
        }

        private void OnEnable()
        {
            _activeShip.OnValueChanged += OnActiveShipChanged;
        }

        private void OnDisable()
        {
            _activeShip.OnValueChanged -= OnActiveShipChanged;
        }

        private void OnActiveShipChanged()
        {
            if (_activeShip.HasValue)
            {
                return;
            }
            
            _selection.TryDeselectCurrent();
        }
    }
}
