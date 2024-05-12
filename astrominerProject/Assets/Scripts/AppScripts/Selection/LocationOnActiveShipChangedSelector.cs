using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class LocationOnActiveShipChangedSelector : MonoBehaviour, Injectable, Initializable, Cleanable
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
            SelectLocation();
        }

        private void SelectLocation()
        {
            if (!_activeShip.HasValue || _activeShip.Value.Location.Value is not CosmicObject cosmicObject)
            {
                return;
            }

            _activeCosmicObject.Value = cosmicObject;
        }
    }
}
