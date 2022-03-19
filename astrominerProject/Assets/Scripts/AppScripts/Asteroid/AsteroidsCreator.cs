using SBaier.DI;
using System.Collections.Generic;
using UnityEngine;

namespace SBaier.Astrominer
{
	public class AsteroidsCreator : MonoBehaviour, Injectable
	{
		[SerializeField]
		private int _asteroidsAmount = 20;
		[SerializeField]
		private Transform _hook;

		private Factory<List<Asteroid>, int> _asteroidsFactory;

		public void Inject(Resolver resolver)
		{
			_asteroidsFactory = resolver.Resolve<Factory<List<Asteroid>, int>>();
		}

		private void Start()
		{
			List<Asteroid> asteroids = _asteroidsFactory.Create(_asteroidsAmount);
			foreach (Asteroid asteroid in asteroids)
			{
				asteroid.Base.SetParent(_hook);
			}
		}
	}
}
