using SBaier.DI;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace SBaier.Astrominer
{
    public class TakeMachineButton : MonoBehaviour, Injectable
    {
		[SerializeField]
		private Button _button;

		private Ship _ship;
		private Asteroid _currentAsteroid;

		public void Inject(Resolver resolver)
		{
			_ship = resolver.Resolve<Ship>();
		}

		private void Start()
		{
			UpdateInteractivity();
			_ship.Location.OnValueChanged += OnFlyTargetChange;
			_button.onClick.AddListener(TakeMachine);
		}

        private void OnDestroy()
		{
			_ship.Location.OnValueChanged -= OnFlyTargetChange;
			_button.onClick.RemoveListener(TakeMachine);
			RemoveCurrentAsteroid();
		}

		private void OnFlyTargetChange(FlyTarget formerValue, FlyTarget newValue)
		{
			UpdateCurrentAsteroid();
			UpdateInteractivity();
		}

		private void UpdateInteractivity()
		{
			_button.interactable = !_ship.Machines.LimitReached &&
				_ship.Location.Value != null &&
				_currentAsteroid != null &&
				_currentAsteroid.HasOwningPlayer &&
				_currentAsteroid.OwningPlayer == _ship.Player &&
				_currentAsteroid.HasExploitMachine;
		}

		private void TakeMachine()
		{
			_currentAsteroid.SetOwningPlayer(null);
			_ship.Machines.Add(_currentAsteroid.TakeExploitMachine());
			UpdateInteractivity();
		}

		private void UpdateCurrentAsteroid()
		{
			if (_ship.Location == null || !(_ship.Location.Value is Asteroid))
				RemoveCurrentAsteroid();
			else
				SetCurrentAsteroid(_ship.Location.Value as Asteroid);
		}

        private void SetCurrentAsteroid(Asteroid asteroid)
        {
			RemoveCurrentAsteroid();
			_currentAsteroid = asteroid;
			_currentAsteroid.OnExploitMachineChanged += UpdateInteractivity;
		}

        private void RemoveCurrentAsteroid()
        {
			if (_currentAsteroid == null)
				return;
			_currentAsteroid.OnExploitMachineChanged -= UpdateInteractivity;
			_currentAsteroid = null;
		}
    }
}
