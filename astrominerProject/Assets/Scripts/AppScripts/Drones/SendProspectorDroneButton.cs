using SBaier.DI;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace SBaier.Astrominer
{
    public class SendProspectorDroneButton : MonoBehaviour, Injectable
    {
        [SerializeField]
        private Button _button;

		private ProspectorDrones _drones;
		private IdentifiedAsteroids _asteroids;
		private Asteroid _target;
		private Ship _ship;
		private Base _base;
		private Player _player;
		private ProspectorDroneBuyer _buyer;
		private ProspectorDroneSettings _settings;

		public void Inject(Resolver resolver)
		{
			_drones = resolver.Resolve<ProspectorDrones>();
			_asteroids = resolver.Resolve<IdentifiedAsteroids>();
			_target = resolver.Resolve<Asteroid>();
			_ship = resolver.Resolve<Ship>();
			_base = resolver.Resolve<Base>();
			_player = resolver.Resolve<Player>();
			_buyer = resolver.Resolve<ProspectorDroneBuyer>();
			_settings = resolver.Resolve<ProspectorDroneSettings>();
		}

		private void OnEnable()
		{
			CheckButtonActive();
			_button.onClick.AddListener(SendDrone);
			_drones.OnItemRemoved += CheckButtonActive;
			_drones.OnItemAdded += CheckButtonActive;
			_asteroids.OnItemAdded += CheckButtonActive;
			_ship.FlyTarget.OnValueChanged += OnFlyTargetChanged;
		}

		private void OnDisable()
		{
			_button.onClick.RemoveListener(SendDrone);
			_drones.OnItemRemoved -= CheckButtonActive;
			_drones.OnItemAdded -= CheckButtonActive;
			_asteroids.OnItemAdded -= CheckButtonActive;
			_ship.FlyTarget.OnValueChanged -= OnFlyTargetChanged;
			_player.Credits.OnAmountChanged += CheckButtonActive;
		}

		private void OnFlyTargetChanged(FlyTarget formervalue, FlyTarget newvalue)
		{
			CheckButtonActive();
		}

		private void CheckButtonActive()
		{
			_button.interactable = GetButtonActive();
		}

		private void CheckButtonActive(ProspectorDrone _)
		{
			_button.interactable = GetButtonActive();
		}

		private void CheckButtonActive(Asteroid _)
		{
			_button.interactable = GetButtonActive();
		}

		private bool GetButtonActive()
		{
			return !_drones.ContainsDroneTo(_target) &&
				!_asteroids.Contains(_target) &&
				!_ship.IsFlying && 
				_settings.Price <= _player.Credits.Amount;
		}

		private void SendDrone()
		{
			_drones.Add(_buyer.BuyDrone(_ship, _target, _base));
		}
	}
}
