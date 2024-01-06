using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class NoActiveShipCosmicObjectDeselector : MonoBehaviour, Injectable
    {
        private ActiveItem<Ship> _activeShip;
        private ActiveItem<CosmicObject> _activeCosmicObject;

        public void Inject(Resolver resolver)
        {
            _activeShip = resolver.Resolve<ActiveItem<Ship>>();
            _activeCosmicObject = resolver.Resolve<ActiveItem<CosmicObject>>();
        }

        private void OnEnable()
        {
            _activeShip.OnValueChanged += OnActiveShipChanged;
        }

        private void OnDisable()
        {
            _activeShip.OnValueChanged -= OnActiveShipChanged;
        }

        private void OnActiveShipChanged(Ship formerValue, Ship newValue)
        {
            if (newValue != null)
            {
                return;
            }

            _activeCosmicObject.Value = null;
        }
    }
}
