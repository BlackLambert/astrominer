using SBaier.DI;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SBaier.Astrominer
{
	public class AsteroidsFactory : Factory<List<Asteroid>, IEnumerable<Vector2>>, Injectable
	{
		private AsteroidSettings _settings;
		private Pool<Asteroid, Asteroid.Arguments> _asteroidsPool;
		private System.Random _random;

		public void Inject(Resolver resolver)
		{
			_settings = resolver.Resolve<AsteroidSettings>();
			_asteroidsPool = resolver.Resolve<Pool<Asteroid, Asteroid.Arguments>>();
			_random = resolver.Resolve<System.Random>();
		}

		public List<Asteroid> Create(IEnumerable<Vector2> positions)
		{
			List<Asteroid> result = new List<Asteroid>();
			List<Vector2> positionsList = positions.ToList();
			for (int i = 0; i < positionsList.Count; i++ )
			{
				Asteroid.Arguments settings = CreateRandomSettings();
				Asteroid asteroid = _asteroidsPool.Request(settings);
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
			AsteroidBodyMaterials bodyMaterial = CalculateBodyMaterial(size, quality);
			return new Asteroid.Arguments(quality, size, _settings.Color, bodyMaterial, _settings.ExploitedColorReduction);
		}

		private AsteroidBodyMaterials CalculateBodyMaterial(int size, int quality)
		{
			float bodyMaterialAmount = size * _settings.BaseRockAmount;
			float totalOresAmount = bodyMaterialAmount * ((float)quality / _settings.MaxQuality);
			float rocksAmount = bodyMaterialAmount - totalOresAmount;
			float oreWeightSum = _settings.OreWeightSum;
			float iron = totalOresAmount * (_settings.IronWeight / oreWeightSum);
			float gold = totalOresAmount * (_settings.GoldWeight / oreWeightSum);
			float platinum = totalOresAmount * (_settings.PlatinumWeight / oreWeightSum);
			Ores ores = new Ores(iron, gold, platinum);
			return new AsteroidBodyMaterials(ores, rocksAmount);
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
