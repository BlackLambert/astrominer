using SBaier.DI;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SBaier.Astrominer
{
	public class AsteroidsFactory : Factory<List<Asteroid>, List<Asteroid.Arguments>>, Injectable
	{
		private AsteroidSettings _settings;
		private Pool<Asteroid, Asteroid.Arguments> _asteroidsPool;

		public void Inject(Resolver resolver)
		{
			_settings = resolver.Resolve<AsteroidSettings>();
			_asteroidsPool = resolver.Resolve<Pool<Asteroid, Asteroid.Arguments>>();
		}

		public List<Asteroid> Create(List<Asteroid.Arguments> arguments)
		{
			List<Asteroid> result = new List<Asteroid>();
			for (int i = 0; i < arguments.Count; i++ )
			{
				Asteroid.Arguments settings = arguments[i];
				Asteroid asteroid = _asteroidsPool.Request(settings);
				asteroid.SetPosition(settings.Position);
				asteroid.SetRotation(settings.Rotation);
				asteroid.SetName(GetName(i));
				asteroid.SetObjectSize(GetObjectSize(settings.Size));
				result.Add(asteroid);
			}
			return result;
		}

		private string GetName(int index)
        {
			return string.Format(_settings.AsteroidName, index);
		}

		private float GetObjectSize(float size)
		{
			float factor = (size - _settings.MinSize) / (_settings.MaxSize - _settings.MinSize);
			float sizeAddition = _settings.MaxObjectSizeAddition * factor;
			return _settings.StartObjectSize + sizeAddition;
		}
	}
}
