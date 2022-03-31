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
			_ship.OnFlyTargetReached += UpdateInteractivity;
			_ship.OnFlyTargetReached += UpdateCurrentAsteroid;
			_ship.OnFlyTargetChanged += UpdateCurrentAsteroid;
			_button.onClick.AddListener(TakeMachine);
		}

        private void OnDestroy()
		{
			_ship.OnFlyTargetReached -= UpdateInteractivity;
			_ship.OnFlyTargetReached -= UpdateCurrentAsteroid;
			_ship.OnFlyTargetChanged -= UpdateCurrentAsteroid;
			_button.onClick.RemoveListener(TakeMachine);
			RemoveCurrentAsteroid();
		}

		private void UpdateInteractivity()
		{
			Debug.Log($"Limit reached? {_ship.Machines.LimitReached} {_ship.Machines.Limit} {_ship.Machines.Count}");
			_button.interactable = !_ship.Machines.LimitReached &&
				!_ship.IsFlying &&
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
			if (_ship.FlyTarget == null || !(_ship.FlyTarget is Asteroid))
				RemoveCurrentAsteroid();
			else
				SetCurrentAsteroid(_ship.FlyTarget as Asteroid);
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
