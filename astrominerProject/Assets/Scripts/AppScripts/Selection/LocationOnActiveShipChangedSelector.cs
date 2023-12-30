using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class LocationOnActiveShipChangedSelector : MonoBehaviour, Injectable
    {
        [SerializeField] 
        private Selectable _selectable;

        private Selection _selection;
        private FlyTarget _flyTarget;
        private ActiveItem<Ship> _activeShip;

        public void Inject(Resolver resolver)
        {
            _selection = resolver.Resolve<Selection>();
            _flyTarget = resolver.Resolve<FlyTarget>();
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

        private void OnActiveShipChanged(Ship formerValue, Ship newValue)
        {
            SelectLocation();
        }

        private void SelectLocation()
        {
            if (!_activeShip.HasValue || _activeShip.Value.Location.Value != _flyTarget)
            {
                return;
            }
            
            _selection.Select(_selectable);
        }
    }
}
