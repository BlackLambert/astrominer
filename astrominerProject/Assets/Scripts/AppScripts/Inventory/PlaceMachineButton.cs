using System;
using SBaier.DI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SBaier.Astrominer
{
    public class PlaceMachineButton : MonoBehaviour, Injectable, Initializable, Cleanable
    {
        [SerializeField]
        private Button _button;

        [SerializeField] 
        private TextMeshProUGUI _label;

        [SerializeField] 
        private string _replaceText = "Replace Machine";

        [SerializeField] 
        private string _placeText = "Place Machine";

		private ActiveItem<ShipInventoryItem> _activeItem;
		private Ship _ship;
		private Asteroid _currentLocation;
		private Observable<FlyTarget> location => _ship.Location;
		private ObservableList<ExploitMachine> machines => _ship.Machines;

		public void Inject(Resolver resolver)
		{
			_activeItem = resolver.Resolve<ActiveItem<ShipInventoryItem>>();
			_ship = resolver.Resolve<Ship>();
		}

		public void Initialize()
		{
			UpdateInteractivity();
			UpdateLabel();
			_activeItem.OnValueChanged += OnActiveItemChanged;
			_ship.Location.OnValueChanged += OnLocationChanged;
			_button.onClick.AddListener(PlaceMachine);
			AddAsteroidListeners();
		}

		public void Clean()
		{
			_activeItem.OnValueChanged -= OnActiveItemChanged;
			_ship.Location.OnValueChanged -= OnLocationChanged;
			_button.onClick.RemoveListener(PlaceMachine);
			RemoveAsteroidListeners();
		}

		private void OnActiveItemChanged(ShipInventoryItem formerValue, ShipInventoryItem newValue)
		{
			UpdateInteractivity();
		}

		private void OnLocationChanged(FlyTarget formervalue, FlyTarget newvalue)
		{
			RemoveAsteroidListeners();
			UpdateLabel();
			UpdateInteractivity();
			AddAsteroidListeners();
		}

		private void AddAsteroidListeners()
		{
			_currentLocation = _ship.Location.Value as Asteroid;
			if (_currentLocation != null)
			{
				_currentLocation.OnExploitMachineChanged += OnExploitMachineChanged;
			}
		}

		private void RemoveAsteroidListeners()
		{
			if (_currentLocation != null)
			{
				_currentLocation.OnExploitMachineChanged -= OnExploitMachineChanged;
			}
		}

		private void OnExploitMachineChanged()
		{
			UpdateLabel();
			UpdateInteractivity();
		}

		private void UpdateLabel()
		{
			_label.text = IsPlayersAsteroid() ? _replaceText : _placeText;
		}

		private bool IsPlayersAsteroid()
		{
			return location.Value is Asteroid { HasOwningPlayer: true } asteroid &&
			       asteroid.OwningPlayer == _ship.Player;
		}
		
		private bool IsEmptyAsteroid()
		{
			return location.Value is Asteroid { HasOwningPlayer: false };
		}

		private void UpdateInteractivity()
		{
			_button.interactable = _activeItem.HasValue && (IsEmptyAsteroid() || IsPlayersAsteroid());
		}

		private void PlaceMachine()
		{
			if (location.Value is not Asteroid asteroid)
			{
				throw new InvalidOperationException($"Placing machine failed" +
				                                    $" since the current ship location is no {nameof(Asteroid)}.");
			}

			ShipInventoryItem selectedItem = _activeItem.Value;
			ExploitMachine machine = selectedItem.Machine;
			if (asteroid.OwningPlayer == _ship.Player)
			{
				ReplaceMachineOf(asteroid, machine);
			}
			else
			{
				PlaceMachineOn(asteroid, machine);
			}
		}

		private void ReplaceMachineOf(Asteroid asteroid, ExploitMachine machine)
		{
			ExploitMachine formerMachine = asteroid.ReplaceExploitMachine(machine);
			machines[machines.IndexOf(machine)] = formerMachine;
		}

		private void PlaceMachineOn(Asteroid asteroid, ExploitMachine machine)
		{
			machines.Remove(machine);
			asteroid.SetOwningPlayer(_ship.Player);
			_ship.Player.OwnedAsteroids.Add(asteroid);
			asteroid.PlaceExploitMachine(machine);
		}
    }
}
