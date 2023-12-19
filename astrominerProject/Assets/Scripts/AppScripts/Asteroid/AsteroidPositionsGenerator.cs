using System.Collections;
using System.Collections.Generic;
using PCGToolkit.Sampling;
using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class AsteroidPositionsGenerator : Injectable
    {
        private PoissonDiskSampling2D _sampler;
        private System.Random _random;

        public void Inject(Resolver resolver)
        {
            _sampler = resolver.Resolve<PoissonDiskSampling2D>();
            _random = resolver.Resolve<System.Random>();
        }

        public List<Vector2> GenerateMap(AstroidAmountOption amountOption, float minDistance)
        {
            Vector2 size = amountOption.MapSize;
            float halfWidth = size.x / 2;
            float halfHeight = size.y / 2;
            Vector2 leftBottom = new Vector2(-halfWidth, -halfHeight);
            RectangleBounds bounds = new RectangleBounds(leftBottom, size);
            Vector2 start = new Vector2(
                (float) _random.NextDouble() * size.x - halfWidth, 
                (float) _random.NextDouble() * size.y - halfHeight);
            return _sampler.Sample(new PoissonDiskSampling2D.Parameters(
                amountOption.Amount, minDistance, bounds, start));
        }
    }
}
