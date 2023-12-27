using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class SelectedObjectConnectionsCreator : MonoBehaviour, Injectable
    {
        private Pool<CosmicObjectConnectionDrawer, MonoBehaviourInRangeDetector2D<CosmicObject>.Arguments> _pool;
        private ActiveItem<Ship> _activeShip;
        private ActiveItem<CosmicObject> _activeCosmicObject;

        private CosmicObjectConnectionDrawer _currentDrawer;
        private bool showConnections => _activeShip.HasValue && _activeCosmicObject.HasValue;
        
        public void Inject(Resolver resolver)
        {
            _pool = resolver
                .Resolve<Pool<CosmicObjectConnectionDrawer, MonoBehaviourInRangeDetector2D<CosmicObject>.Arguments>>();
            _activeCosmicObject = resolver.Resolve<ActiveItem<CosmicObject>>();
            _activeShip = resolver.Resolve<ActiveItem<Ship>>();
        }

        private void OnEnable()
        {
            UpdateDrawer();
            _activeCosmicObject.OnValueChanged += OnActiveCosmicObjectChanged;
            _activeShip.OnValueChanged += OnActiveShipChanged;
        }

        private void OnDisable()
        {
            _activeCosmicObject.OnValueChanged -= OnActiveCosmicObjectChanged;
            _activeShip.OnValueChanged -= OnActiveShipChanged;
        }

        private void OnActiveCosmicObjectChanged(CosmicObject formerValue, CosmicObject newValue)
        {
            UpdateDrawer();
        }

        private void OnActiveShipChanged(Ship formerValue, Ship newValue)
        {
            UpdateDrawer();
        }

        private void UpdateDrawer()
        {
            TryRemoveDrawer();
            TryCreateDrawer();
        }

        private void TryRemoveDrawer()
        {
            if (_currentDrawer == null)
            {
                return;
            }

            _pool.Return(_currentDrawer);
            _currentDrawer = null;
        }

        private void TryCreateDrawer()
        {
            if (!showConnections)
            {
                return;
            }
            
            _currentDrawer = _pool.Request(new MonoBehaviourInRangeDetector2D<CosmicObject>.Arguments()
            {
                Distance = _activeShip.Value.Range,
                StartPoint = _activeCosmicObject.Value.transform
            });
        }
    }
}
