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

        public List<Vector2> GenerateMap(AsteroidAmountOption amountOption, Vector2 centerPoint, float minDistance)
        {
            Vector2 size = amountOption.MapSize;
            float halfWidth = size.x / 2;
            float halfHeight = size.y / 2;
            Vector2 leftBottom = new Vector2(-halfWidth + centerPoint.x, -halfHeight + centerPoint.y);
            RectangleBounds bounds = new RectangleBounds(leftBottom, size);
            Vector2 start = GetRandomStartPosition(size, leftBottom);
            List<Vector2> positions = _sampler.Sample(new PoissonDiskSampling2D.Parameters(
                amountOption.Amount, minDistance, bounds, start));
            return CenterPositions(positions, centerPoint);
        }

        private Vector2 GetRandomStartPosition(Vector2 size, Vector2 leftBottom)
        {
            return new Vector2(
                (float)_random.NextDouble() * size.x + leftBottom.x,
                (float)_random.NextDouble() * size.y + leftBottom.y);
        }

        private List<Vector2> CenterPositions(List<Vector2> positions, Vector2 center)
        {
            List<Vector2> result = new List<Vector2>();
            Vector2 first = positions.Count > 0 ? positions[0] : Vector2.zero;
            Vector2 xExtremes = new Vector2(first.x, first.x);
            Vector2 yExtremes = new Vector2(first.y, first.y);

            foreach (Vector2 position in positions)
            {
                if (position.x < xExtremes.x)
                {
                    xExtremes.x = position.x;
                }
                
                if (position.x > xExtremes.y)
                {
                    xExtremes.y = position.x;
                }
                
                if (position.y < yExtremes.x)
                {
                    yExtremes.x = position.y;
                }
                
                if (position.y > yExtremes.y)
                {
                    yExtremes.y = position.y;
                }
            }

            float x = xExtremes.x + (xExtremes.y - xExtremes.x) / 2;
            float y = yExtremes.x + (yExtremes.y - yExtremes.x) / 2;
            Vector2 delta = new Vector2(x, y) - center;

            foreach (Vector2 position in positions)
            {
                result.Add(position - delta);
            }

            return result;
        }
    }
}