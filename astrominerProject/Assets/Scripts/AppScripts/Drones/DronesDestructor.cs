using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
	public class DronesDestructor : MonoBehaviour, Injectable
	{
		private ProspectorDrones _drones;

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
			drone.OnDone += Destruct;
		}

		private void Destruct(ProspectorDrone drone)
		{
			drone.OnDone -= Destruct;
			_drones.Remove(drone);
			Destroy(drone.gameObject);
		}
	}
}
