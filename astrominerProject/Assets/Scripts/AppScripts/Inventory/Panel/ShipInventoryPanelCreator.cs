using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class ShipInventoryPanelCreator : MonoBehaviour, Injectable, Initializable, Cleanable
    {
        [SerializeField] private Transform _hook;

        private ActiveShip _activeShip;
        private Pool<ShipInventoryPanel, Ship, PrefabInstantiationArguments> _pool;

        private ShipInventoryPanel _currentPanel;

        public void Inject(Resolver resolver)
        {
            _activeShip = resolver.Resolve<ActiveShip>();
            _pool = resolver.Resolve<Pool<ShipInventoryPanel, Ship, PrefabInstantiationArguments>>();
        }

        public void Initialize()
        {
            TryCreatePanel();
            _activeShip.OnValueChanged += OnActiveShipChanged;
        }

        public void Clean()
        {
            _activeShip.OnValueChanged -= OnActiveShipChanged;
        }

        private void OnActiveShipChanged(Ship formerValue, Ship newValue)
        {
            UpdatePanel();
        }

        private void UpdatePanel()
        {
            TryReturnPanel();
            TryCreatePanel();
        }

        private void TryReturnPanel()
        {
            if (_currentPanel == null)
                return;
            _currentPanel.InvokeOnPool();
            _pool.Return(_currentPanel);
            _currentPanel = null;
        }

        private void TryCreatePanel()
        {
            if (!_activeShip.HasValue)
                return;
            _currentPanel = _pool.Request(_activeShip.Value, PrefabInstantiationArguments.CreateFittedUIArgs(_hook));
        }
    }
}