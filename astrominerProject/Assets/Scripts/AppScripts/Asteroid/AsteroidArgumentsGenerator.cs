using System.Collections.Generic;
using System.Linq;
using PCGToolkit.Sampling;
using SBaier.DI;
using UnityEngine;
using Random = System.Random;

namespace SBaier.Astrominer
{
    public class AsteroidArgumentsGenerator : Injectable
    {
        private AsteroidSettings _settings;
        private PoissonDiskSampling2D _sampler;
        private Random _random;

        public void Inject(Resolver resolver)
        {
            _settings = resolver.Resolve<AsteroidSettings>();
            _sampler = resolver.Resolve<PoissonDiskSampling2D>();
            _random = resolver.Resolve<Random>();
            _random = _random.CreateWithNewSeed();
        }

        public List<Asteroid.Arguments> GenerateMap(AsteroidAmountOption amountOption, float minDistance)
        {
            Vector2 size = amountOption.MapSize;
            Vector2 centerPoint = amountOption.MapCenterPoint;
            float halfWidth = size.x / 2;
            float halfHeight = size.y / 2;
            Vector2 leftBottom = new Vector2(-halfWidth + centerPoint.x, -halfHeight + centerPoint.y);
            RectangleBounds bounds = new RectangleBounds(leftBottom, size);
            Vector2 start = GetRandomStartPosition(size, leftBottom);
            List<Vector2> positions = _sampler.Sample(new PoissonDiskSampling2D.Parameters(
                amountOption.Amount, minDistance, bounds, start));
            positions = CenterPositions(positions, centerPoint);
            return positions.Select(CreateRandomSettings).ToList();
        }

        private Vector2 GetRandomStartPosition(Vector2 size, Vector2 leftBottom)
        {
            return new Vector2(
                (float)_random.NextDouble() * size.x + leftBottom.x,
                (float)_random.NextDouble() * size.y + leftBottom.y);
        }

        private Quaternion GetRandomRotation()
        {
            float rotation = (float) _random.NextDouble() * 360;
            return Quaternion.Euler(0, 0, rotation);
        }

        private Asteroid.Arguments CreateRandomSettings(Vector2 position)
        {
            int quality = _random.Next(_settings.MinQuality, _settings.MaxQuality + 1);
            int size = _random.Next(_settings.MinSize, _settings.MaxSize + 1);
            AsteroidBodyMaterials bodyMaterial = CalculateBodyMaterial(size, quality);
            return new Asteroid.Arguments(position, GetRandomRotation(), quality, size, _settings.Color, bodyMaterial, _settings.ExploitedColorReduction);
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