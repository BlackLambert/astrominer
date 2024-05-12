using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class CarryingOresPanelCreator : MonoBehaviour, Injectable, Initializable, Cleanable
    {
        [SerializeField] private Transform _hook;

        private ActiveShip _activeShip;
        private Pool<CarryingOresPanel, Ship, PrefabInstantiationArguments> _pool;

        private CarryingOresPanel _currentPanel;

        public void Inject(Resolver resolver)
        {
            _activeShip = resolver.Resolve<ActiveShip>();
            _pool = resolver.Resolve<Pool<CarryingOresPanel, Ship, PrefabInstantiationArguments>>();
        }

        public void Initialize()
        {
            TryCreatePanel();
            _activeShip.OnValueChanged += OnActiveShipChanged;
        }

        public void Clean()
        {
            _activeShip.OnValueChanged -= OnActiveShipChanged;
            TryReturnPanel();
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