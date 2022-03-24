using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
	public class AsteroidIdentifier : MonoBehaviour, Injectable
	{
		private Flyable _flyable;
		private IdentifiedAsteroids _asteroids;

		public void Inject(Resolver resolver)
		{
			_flyable = resolver.Resolve<Flyable>();
			_asteroids = resolver.Resolve<IdentifiedAsteroids>();
		}

		private void Start()
		{
			_flyable.OnFlyTargetReached += TryIdentifyAsteroid;
		}

		private void OnDestroy()
		{
			_flyable.OnFlyTargetReached -= TryIdentifyAsteroid;
		}

		private void TryIdentifyAsteroid()
		{
			if (_flyable.FlyTarget is Asteroid asteroid && !_asteroids.Contains(asteroid))
				_asteroids.Add(asteroid);
		}
	}
}