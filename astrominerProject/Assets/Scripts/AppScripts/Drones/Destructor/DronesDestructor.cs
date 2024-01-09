using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
	public class DronesDestructor : MonoBehaviour, Injectable
	{
		private Drones _drones;

		public void Inject(Resolver resolver)
		{
			_drones = resolver.Resolve<Drones>();
		}

		private void Start()
		{
			_drones.OnItemAdded += AddDestructor;
		}

		private void OnDestroy()
		{
			_drones.OnItemAdded -= AddDestructor;
		}

		private void AddDestructor(Drone drone)
		{
			drone.OnDone += Destruct;
		}

		private void Destruct(Drone drone)
		{
			drone.OnDone -= Destruct;
			_drones.Remove(drone);
			Destroy(drone.gameObject);
		}
	}
}
