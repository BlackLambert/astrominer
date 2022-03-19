using SBaier.DI;
using System.Collections.Generic;
using UnityEngine;

namespace SBaier.Astrominer
{
	public class AsteroidsFactory : Factory<List<Asteroid>, int>, Injectable
	{
		private const string _asteroidName = "Asteroid {0}";

		private AsteroidSettings _config;
		private Factory<Asteroid, Asteroid.Arguments> _asteroidFactory;
		private System.Random _random;

		public void Inject(Resolver resolver)
		{
			_config = resolver.Resolve<AsteroidSettings>();
			_asteroidFactory = resolver.Resolve<Factory<Asteroid, Asteroid.Arguments>>();
			_random = resolver.Resolve<System.Random>();
		}

		public List<Asteroid> Create(int amount)
		{
			List<Asteroid> result = new List<Asteroid>();
			for (int i = 0; i < amount; i++)
			{
				Asteroid.Arguments settings = CreateRandomSettings();
				Asteroid asteroid = _asteroidFactory.Create(settings);
				asteroid.SetPosition(GetRandomPosition());
				asteroid.SetName(GetName(i));
				asteroid.SetSize(GetRandomSize());
				result.Add(asteroid);
			}
			return result;
		}

        private Asteroid.Arguments CreateRandomSettings()
		{
			float resourceAmount = _config.MinResourceAmount;
			resourceAmount += (float)_random.NextDouble() * (_config.MaxResourceAmount - _config.MinResourceAmount);
			return new Asteroid.Arguments(resourceAmount);
		}

		private Vector2 GetRandomPosition()
		{
			float x = _config.MinPosition.x;
			x += (float)_random.NextDouble() * (_config.MaxPosition.x - _config.MinPosition.x);
			float y = _config.MinPosition.y;
			y += (float)_random.NextDouble() * (_config.MaxPosition.y - _config.MinPosition.y);
			return new Vector2(x, y);
		}

		private float GetRandomSize()
		{
			float size = _config.MinSize;
			size += (float)_random.NextDouble() * (_config.MaxSize - _config.MinSize);
			return size;
		}

		private string GetName(int index)
        {
			return string.Format(_asteroidName, index);

		}
	}
}
