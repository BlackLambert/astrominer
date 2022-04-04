using SBaier.DI;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SBaier.Astrominer
{
	public class AsteroidsFactory : Factory<List<Asteroid>, IEnumerable<Vector2>>, Injectable
	{
		private const string _asteroidName = "Asteroid {0}";
		private const float _maxObjectSizeAddition = 0.5f;
		private const float _startObjectSize = 0.8f;

		private AsteroidSettings _settings;
		private Factory<Asteroid, Asteroid.Arguments> _asteroidFactory;
		private System.Random _random;

		public void Inject(Resolver resolver)
		{
			_settings = resolver.Resolve<AsteroidSettings>();
			_asteroidFactory = resolver.Resolve<Factory<Asteroid, Asteroid.Arguments>>();
			_random = resolver.Resolve<System.Random>();
		}

		public List<Asteroid> Create(IEnumerable<Vector2> positions)
		{
			List<Asteroid> result = new List<Asteroid>();
			List<Vector2> positionsList = positions.ToList();
			for (int i = 0; i < positionsList.Count; i++ )
			{
				Asteroid.Arguments settings = CreateRandomSettings();
				Asteroid asteroid = _asteroidFactory.Create(settings);
				asteroid.SetPosition(positionsList[i]);
				asteroid.SetRotation(GetRandomRotation());
				asteroid.SetName(GetName(i));
				asteroid.SetObjectSize(GetObjectSize(settings.Size));
				result.Add(asteroid);
			}
			return result;
		}

		private Quaternion GetRandomRotation()
		{
			float rotation = (float) _random.NextDouble() * 360;
			return Quaternion.Euler(0, 0, rotation);
		}

		private Asteroid.Arguments CreateRandomSettings()
		{
			int quality = _random.Next(_settings.MinQuality, _settings.MaxQuality + 1);
			int size = _random.Next(_settings.MinSize, _settings.MaxSize + 1);
			Ores ores = CalculateExploitableOres(size);
			return new Asteroid.Arguments(quality, size, _settings.Color, ores, _settings.ExploitedColorReduction);
		}

		private Ores CalculateExploitableOres(int size)
		{
			float allOresAmount = size * _settings.BaseOreAmount;
			float oreWeightSum = _settings.OreWeightSum;
			float iron = allOresAmount * (_settings.IronWeight / oreWeightSum);
			float gold = allOresAmount * (_settings.GoldWeight / oreWeightSum);
			float platinum = allOresAmount * (_settings.PlatinumWeight / oreWeightSum);
			return new Ores(iron, gold, platinum);
		}

		private string GetName(int index)
        {
			return string.Format(_asteroidName, index);
		}

		private float GetObjectSize(float size)
		{
			float factor = (size - _settings.MinSize) / (_settings.MaxSize - _settings.MinSize);
			float sizeAddition = _maxObjectSizeAddition * factor;
			return _startObjectSize + sizeAddition;
		}
	}
}
