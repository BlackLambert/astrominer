using SBaier.DI;
using System.Collections.Generic;
using UnityEngine;

namespace SBaier.Astrominer
{
	public class RandomAsteroidsCreator : AsteroidsCreator
	{
		[SerializeField]
		private int _asteroidsAmount = 20;

		private AsteroidSettings _config;
		private System.Random _random;

		public override void Inject(Resolver resolver)
		{
			base.Inject(resolver);
			_config = resolver.Resolve<AsteroidSettings>();
			_random = resolver.Resolve<System.Random>();
		}

		protected override IEnumerable<Vector2> GetPositions()
		{
			List<Vector2> result = new List<Vector2>();
			for(int i = 0; i < _asteroidsAmount; i++)
				result.Add(GetRandomPosition());
			return result;
		}

		private Vector2 GetRandomPosition()
		{
			float x = _config.MinPosition.x;
			x += (float)_random.NextDouble() * (_config.MaxPosition.x - _config.MinPosition.x);
			float y = _config.MinPosition.y;
			y += (float)_random.NextDouble() * (_config.MaxPosition.y - _config.MinPosition.y);
			return new Vector2(x, y);
		}
	}
}
