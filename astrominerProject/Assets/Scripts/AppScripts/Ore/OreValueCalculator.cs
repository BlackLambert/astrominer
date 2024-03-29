using System.Collections.Generic;
using PCGToolkit.Sampling;
using SBaier.DI;
using UnityEngine;
using Random = System.Random;

namespace SBaier.Astrominer
{
    public class OreValueCalculator : MonoBehaviour, Injectable
    {
        private OreType _type;
        private OreValue _oreValue;
        private Random _random;
        private OresSettings _oresSettings;
        private OresSettings.OreSettings _oreSettings;
        private GameTime _gameTime;
        private List<Step> _steps;
        private float _minValue;
        private float _valueRange;
        private CircularBuffer<float> _formerValue;

        public void Inject(Resolver resolver)
        {
            _type = resolver.Resolve<OreType>();
            _oreValue = resolver.Resolve<OreValue>();
            _random = resolver.Resolve<Random>();
            _random = _random.CreateWithNewSeed();
            _oresSettings = resolver.Resolve<OresSettings>();
            _gameTime = resolver.Resolve<GameTime>();
        }

        private void Start()
        {
            _oreSettings = _oresSettings.Get(_type);
            _minValue = _oreSettings.PriceRange.x;
            _valueRange = _oreSettings.PriceRange.y - _minValue;
            _formerValue = _oreValue.GetFormerValue(_type);
            
            _oreValue.OnShallUpdateValue += CalculateValue;
            CreateNoiseSteps();
            CalculateValue();
        }

        private void OnDestroy()
        {
            _oreValue.OnShallUpdateValue -= CalculateValue;
        }

        private void CreateNoiseSteps()
        {
            _steps = new List<Step>(_oresSettings.OreValueNoiseSteps.Count);
            foreach (OresSettings.NoiseStep step in _oresSettings.OreValueNoiseSteps)
            {
                Random random = _random.CreateWithNewSeed();
                float offset = (float)(random.NextDouble() * step.MaxOffset);
                SimplexNoise1D noise = SimplexNoise1D.Create(random);
                _steps.Add(new Step()
                {
                    Frequency = step.Frequency,
                    Noise = noise,
                    Weight = step.Weight,
                    Offset = offset
                });
            }
        }

        private void CalculateValue()
        {
            float value = CalculateValue(_gameTime.Value);
            _oreValue.SetCurrentValue(_type, value);
            _formerValue.Push(value);
        }

        private float CalculateValue(float time)
        {
            float maxValue = 0;
            float value = 0;
            
            foreach (Step step in _steps)
            {
                maxValue += step.Weight;
                value += step.Noise.Evaluate((time + step.Offset) * step.Frequency) * step.Weight;
            }

            float normalized = value / maxValue;
            float normalized01 = (normalized + 1) / 2;
            return _minValue + _valueRange * normalized01;
        }

        private class Step
        {
            public SimplexNoise1D Noise;
            public float Frequency;
            public float Weight;
            public float Offset;
        }
    }
}
