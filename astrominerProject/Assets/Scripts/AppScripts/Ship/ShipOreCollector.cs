using SBaier.DI;
using System;
using UnityEngine;

namespace SBaier.Astrominer
{
	public class ShipOreCollector : MonoBehaviour, Injectable
	{
		private Ship _ship;

		public void Inject(Resolver resolver)
		{
			_ship = resolver.Resolve<Ship>();
		}

		private void Start()
		{
			_ship.OnLocationChanged += TryCollectOre;
		}

		private void OnDestroy()
		{
			_ship.OnLocationChanged -= TryCollectOre;
		}

		private void TryCollectOre()
		{
			if (_ship.Location is Asteroid asteroid &&
				asteroid.OwningPlayer == _ship.Player)
				CollectOre(asteroid);
		}

		private void CollectOre(Asteroid asteroid)
		{
			_ship.CollectedOres.Add(asteroid.Collect());
		}
	}
}
