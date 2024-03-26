using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SBaier.Astrominer
{
    [CreateAssetMenu(fileName = "OreSettings", menuName = "ScriptableObjects/OreSettings")]
    public class OresSettings : ScriptableObject
    {
        [field: SerializeField] 
        public float OreValueUpdateFrequency { get; private set; } = 1;
        
        [SerializeField]
        private List<OreSettings> _ores = new List<OreSettings>();

        [SerializeField] 
        private List<NoiseStep> _oreValueNoiseSteps = new List<NoiseStep>()
        {
            new NoiseStep()
        };

        public IReadOnlyList<NoiseStep> OreValueNoiseSteps => _oreValueNoiseSteps;

        public OreSettings Get(OreType type)
		{
            OreSettings result = _ores.FirstOrDefault(o => o.Type == type);
            if (result == null)
            {
                throw new ArgumentException($"There are no settings for the ore of type {type}");
            }

            return result;
        }

        [Serializable]
        public class OreSettings
		{
            [field: SerializeField]
            public OreType Type { get; private set; } = OreType.None;
            [field: SerializeField]
            public Vector2 PriceRange { get; private set; } = new Vector2(5, 10);
            [field: SerializeField]
            public string DisplayString { get; private set; } = "Ore: {0}";
            [field: SerializeField] 
            public Color Color { get; private set; } = Color.white;
        }

        [Serializable]
        public class NoiseStep
        {
            [field: SerializeField] 
            public float Frequency { get; private set; } = 1;
            [field: SerializeField] 
            public float Weight { get; private set; } = 1;
            [field: SerializeField] 
            public float MaxOffset { get; private set; } = 1_000_000;
        }
    }
}
