using SBaier.DI;
using System;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class ShipInventoryPanelCreator : MonoBehaviour, Injectable
    {
        [SerializeField]
        private Transform _hook;

		private ActiveShip _activeShip;
		private Factory<ShipInventoryPanel, Ship> _factory;

		private ShipInventoryPanel _currentPanel;

		public void Inject(Resolver resolver)
		{
			_activeShip = resolver.Resolve<ActiveShip>();
			_factory = resolver.Resolve<Factory<ShipInventoryPanel, Ship>>();
		}

		private void Start()
		{
			UpdatePanel();
			_activeShip.OnValueChanged += UpdatePanel;
		}

		private void OnDestroy()
		{
			_activeShip.OnValueChanged -= UpdatePanel;
		}

		private void UpdatePanel()
		{
			TryDestructPanel();
			TryCreatePanel();
		}

		private void TryDestructPanel()
		{
			if (_currentPanel == null)
				return;
			Destroy(_currentPanel.gameObject);
			_currentPanel = null;
		}

		private void TryCreatePanel()
		{
			if (!_activeShip.HasValue)
				return;
			_currentPanel = _factory.Create(_activeShip.Value);
			_currentPanel.transform.SetParent(_hook, false);
		}
	}
}
