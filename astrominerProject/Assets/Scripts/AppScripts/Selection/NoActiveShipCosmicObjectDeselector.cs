using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class NoActiveShipCosmicObjectDeselector : MonoBehaviour, Injectable, Initializable, Cleanable
    {
        private ActiveItem<Ship> _activeShip;
        private ActiveItem<CosmicObject> _activeCosmicObject;

        public void Inject(Resolver resolver)
        {
            _activeShip = resolver.Resolve<ActiveItem<Ship>>();
            _activeCosmicObject = resolver.Resolve<ActiveItem<CosmicObject>>();
        }

        public void Initialize()
        {
            _activeShip.OnValueChanged += OnActiveShipChanged;
        }

        public void Clean()
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
