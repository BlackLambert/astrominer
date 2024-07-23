using System.Collections.Generic;
using System.Text;
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
            List<Asteroid.Arguments> result = new List<Asteroid.Arguments>();
            for (var index = 0; index < positions.Count; index++)
            {
                Vector2 position = positions[index];
                result.Add(CreateRandomSettings(index, position));
            }

            LogResult(result);
            return result;
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

        private Asteroid.Arguments CreateRandomSettings(int index, Vector2 position)
        {
            int quality = SampleDistributedValue(_settings.MinQuality, _settings.MaxQuality, _settings.QualityDistribution);
            int size = SampleDistributedValue(_settings.MinSize, _settings.MaxSize, _settings.SizeDistribution);
            AsteroidBodyMaterials bodyMaterial = CalculateBodyMaterial(size, quality);
            return new Asteroid.Arguments(index, position, GetRandomRotation(), quality, size, _settings.Color, bodyMaterial, _settings.ExploitedColorReduction);
        }

        private int SampleDistributedValue(int min, int max, AnimationCurve curve)
        {
            float evaluationValue = (float)_random.NextDouble();
            float factor = curve.Evaluate(evaluationValue);
            float quality = Mathf.Lerp(min, max, factor);
            return Mathf.RoundToInt(quality);
        }

        private AsteroidBodyMaterials CalculateBodyMaterial(int size, int quality)
        {
            float bodyMaterialAmount = size * _settings.BaseRockAmount;
            float totalOresAmount = bodyMaterialAmount * ((float)quality / _settings.MaxQuality);
            float rocksAmount = bodyMaterialAmount - totalOresAmount;
            float ironWeight = _settings.IronWeight.Evaluate((float)_random.NextDouble());
            float goldWeight = _settings.GoldWeight.Evaluate((float)_random.NextDouble());
            float platinumWeight = _settings.PlatinumWeight.Evaluate((float)_random.NextDouble());
            float oreWeightSum = ironWeight + goldWeight + platinumWeight;
            float iron = totalOresAmount * (ironWeight / oreWeightSum);
            float gold = totalOresAmount * (goldWeight / oreWeightSum);
            float platinum = totalOresAmount * (platinumWeight / oreWeightSum);
            Ores ores = new Ores(iron, gold, platinum);
            return new AsteroidBodyMaterials(ores, rocksAmount);
        }

        private void LogResult(List<Asteroid.Arguments> result)
        {
            if (!_settings.LogCreation)
            {
                return;
            }

            Dictionary<int, int> qualityToAmount = new Dictionary<int, int>();
            for (int i = _settings.MinQuality; i <= _settings.MaxQuality; i++)
            {
                qualityToAmount[i] = 0;
            }
            
            Dictionary<int, int> sizeToAmount = new Dictionary<int, int>();
            for (int i = _settings.MinSize; i <= _settings.MaxSize; i++)
            {
                sizeToAmount[i] = 0;
            }

            foreach (Asteroid.Arguments arguments in result)
            {
                qualityToAmount[arguments.Quality]++;
                sizeToAmount[arguments.Size]++;
            }
            
            Debug.Log($"{result.Count} asteroids created");
            LogValues(qualityToAmount, "Quality");
            LogValues(sizeToAmount, "Size");
        }

        private void LogValues(Dictionary<int, int> values, string valueName)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append($"{valueName} distribution:\n");
            foreach (KeyValuePair<int,int> pair in values)
            {
                stringBuilder.Append($"{valueName} {pair.Key}: {pair.Value}\n");
            }
            Debug.Log(stringBuilder.ToString());
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