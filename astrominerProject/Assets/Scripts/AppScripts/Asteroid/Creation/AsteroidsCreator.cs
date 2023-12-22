using SBaier.DI;
using System.Collections.Generic;
using UnityEngine;

namespace SBaier.Astrominer
{
    public abstract class AsteroidsCreator : MonoBehaviour, Injectable
	{
        [SerializeField]
        private Transform _hook;

		private Factory<List<Asteroid>, IEnumerable<Vector2>> _asteroidsFactory;
		private Map _map;

		public virtual void Inject(Resolver resolver)
		{
			_asteroidsFactory = resolver.Resolve<Factory<List<Asteroid>, IEnumerable<Vector2>>>();
			_map = resolver.Resolve<Map>();
		}

		private void Start()
		{
			List<Asteroid> asteroids = _asteroidsFactory.Create(GetPositions());
			
			foreach (Asteroid asteroid in asteroids)
			{
				asteroid.Base.SetParent(_hook);
			}

			_map.Asteroids.Value = asteroids;
		}

		protected abstract IEnumerable<Vector2> GetPositions();
	}
}
