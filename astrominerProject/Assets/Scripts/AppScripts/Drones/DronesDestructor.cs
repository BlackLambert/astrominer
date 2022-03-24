using SBaier.DI;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace SBaier.Astrominer
{
	public class DronesDestructor : MonoBehaviour, Injectable
	{
		private ProspectorDrones _drones;

		private Dictionary<ProspectorDrone, Action> _callbacks =
			new Dictionary<ProspectorDrone, Action>();

		public void Inject(Resolver resolver)
		{
			_drones = resolver.Resolve<ProspectorDrones>();
		}

		private void Start()
		{
			_drones.OnItemAdded += AddDestructor;
		}

		private void OnDestroy()
		{
			_drones.OnItemAdded -= AddDestructor;
		}

		private void AddDestructor(ProspectorDrone drone)
		{
			Action destruct = () => Destruct(drone);
			drone.OnDone += destruct;
			_callbacks[drone] = destruct;
		}

		private void Destruct(ProspectorDrone drone)
		{
			drone.OnDone -= _callbacks[drone];
			_drones.Remove(drone);
			_callbacks.Remove(drone);
			Destroy(drone.gameObject);
		}
	}
}
