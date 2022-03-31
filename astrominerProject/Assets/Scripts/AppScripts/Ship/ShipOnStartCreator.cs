using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
	public class ShipOnStartCreator : MonoBehaviour, Injectable
	{
		private Base _base;
		private Factory<Ship> _shipFactory;
		private ActiveShip _activeShip;
		private Ships _ships;

		public void Inject(Resolver resolver)
		{
			_base = resolver.Resolve<Base>();
			_shipFactory = resolver.Resolve<Factory<Ship>>();
			_activeShip = resolver.Resolve<ActiveShip>();
			_ships = resolver.Resolve<Ships>();
		}

		private void Start()
		{
			Ship ship = _shipFactory.Create();
			ship.transform.position = _base.transform.position;
			ship.transform.rotation = Quaternion.Euler(0, 90, 0);
			ship.FlyTo(_base);
			_ships.Values.Add(ship);
			_activeShip.Value = ship;
		}
	}
}
