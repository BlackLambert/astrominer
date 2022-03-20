using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
	public class ShipOnStartCreator : MonoBehaviour, Injectable
	{
		private Base _base;
		private Factory<Ship> _shipFactory;
		private ActiveShip _activeShip;

		public void Inject(Resolver resolver)
		{
			_base = resolver.Resolve<Base>();
			_shipFactory = resolver.Resolve<Factory<Ship>>();
			_activeShip = resolver.Resolve<ActiveShip>();
		}

		private void Start()
		{
			Ship ship = _shipFactory.Create();
			ship.transform.position = _base.transform.position;
			ship.transform.rotation = Quaternion.Euler(0, 90, 0);
			ship.FlyTo(_base.FlyTarget);
			_activeShip.Value = ship;
		}
	}
}
