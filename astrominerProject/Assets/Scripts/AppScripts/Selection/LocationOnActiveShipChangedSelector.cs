using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class LocationOnActiveShipChangedSelector : MonoBehaviour, Injectable
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
