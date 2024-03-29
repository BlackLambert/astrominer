using SBaier.DI;
using UnityEngine;
using UnityEngine.UI;

namespace SBaier.Astrominer
{
    public class PlaceMachineButton : MonoBehaviour, Injectable
    {
        [SerializeField]
        private Button _button;

		private ActiveItem<ShipInventoryItem> _activeItem;
		private Ship _ship;

		public void Inject(Resolver resolver)
		{
			_activeItem = resolver.Resolve<ActiveItem<ShipInventoryItem>>();
			_ship = resolver.Resolve<Ship>();
		}

		private void OnEnable()
		{
			UpdateInteractivity();
			_activeItem.OnValueChanged += OnActiveItemChanged;
			_button.onClick.AddListener(PlaceMachine);
		}

		private void OnDisable()
		{
			_activeItem.OnValueChanged -= OnActiveItemChanged;
			_button.onClick.RemoveListener(PlaceMachine);
		}

		private void OnActiveItemChanged(ShipInventoryItem formerValue, ShipInventoryItem newValue)
		{
			UpdateInteractivity();
		}

		private void UpdateInteractivity()
		{
			_button.interactable = 
				_activeItem.HasValue &&
			    _ship.Location.Value is Asteroid asteroid &&
				!asteroid.HasOwningPlayer;
		}

		private void PlaceMachine()
		{
			Asteroid asteroid = _ship.Location.Value as Asteroid;
			ShipInventoryItem selectedItem = _activeItem.Value;
			ExploitMachine machine = selectedItem.Machine;
			asteroid.SetOwningPlayer(_ship.Player);
			_ship.Machines.Remove(machine);
			asteroid.PlaceExploitMachine(machine);
			UpdateInteractivity();
		}
	}
}
