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
		private Factory<ProspectorDrone, DroneArguments> _factory;

		public void Inject(Resolver resolver)
		{
			_drones = resolver.Resolve<ProspectorDrones>();
			_asteroids = resolver.Resolve<IdentifiedAsteroids>();
			_target = resolver.Resolve<Asteroid>();
			_ship = resolver.Resolve<Ship>();
			_base = resolver.Resolve<Base>();
			_factory = resolver.Resolve<Factory<ProspectorDrone, DroneArguments>>();
		}

		private void Start()
		{
			CheckButtonActive();
			_button.onClick.AddListener(SendDrone);
			_drones.OnItemRemoved += CheckButtonActive;
			_drones.OnItemAdded += CheckButtonActive;
			_asteroids.OnItemAdded += CheckButtonActive;
			_ship.OnFlyTargetChanged += CheckButtonActive;
		}

		private void OnDestroy()
		{
			_button.onClick.RemoveListener(SendDrone);
			_drones.OnItemRemoved -= CheckButtonActive;
			_drones.OnItemAdded -= CheckButtonActive;
			_asteroids.OnItemAdded -= CheckButtonActive;
			_ship.OnFlyTargetChanged -= CheckButtonActive;
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
				!_ship.IsFlying;
		}

		private void SendDrone()
		{
			Vector2 startPosition = _ship.Position2D;
			DroneArguments settings = new DroneArguments(startPosition, _target, _base);
			ProspectorDrone drone = _factory.Create(settings);
			drone.transform.position = startPosition;
			_drones.Add(drone);
		}
	}
}
