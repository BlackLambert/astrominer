using SBaier.DI;
using System;
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
		private Player _player;

		public void Inject(Resolver resolver)
		{
			_activeItem = resolver.Resolve<ActiveItem<ShipInventoryItem>>();
			_ship = resolver.Resolve<Ship>();
			_player = resolver.Resolve<Player>();
		}

		private void Start()
		{
			UpdateInteractivity();
			_activeItem.OnValueChanged += UpdateInteractivity;
			_ship.OnFlyTargetReached += UpdateInteractivity;
			_ship.OnFlyTargetChanged += UpdateInteractivity;
			_button.onClick.AddListener(PlaceMachine);
		}

		private void OnDestroy()
		{
			_activeItem.OnValueChanged -= UpdateInteractivity;
			_ship.OnFlyTargetReached -= UpdateInteractivity;
			_ship.OnFlyTargetChanged -= UpdateInteractivity;
			_button.onClick.RemoveListener(PlaceMachine);
		}

		private void UpdateInteractivity()
		{
			_button.interactable = _activeItem.HasValue &&
				!_ship.IsFlying &&
				_ship.FlyTarget is Asteroid asteroid &&
				!asteroid.HasOwningPlayer;
		}

		private void PlaceMachine()
		{
			Asteroid asteroid = _ship.FlyTarget as Asteroid;
			ShipInventoryItem selectedItem = _activeItem.Value;
			ExploitMachine machine = selectedItem.Machine;
			asteroid.SetOwningPlayer(_player);
			asteroid.PlaceExploitMachine(machine);
			_ship.Machines.Remove(machine);
			UpdateInteractivity();
		}
	}
}
