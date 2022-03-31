using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SBaier.Astrominer
{
    [CreateAssetMenu(fileName = "OreSettings", menuName = "ScriptableObjects/OreSettings")]
    public class OresSettings : ScriptableObject
    {
        [SerializeField]
        private List<OreSettings> _ores = new List<OreSettings>();

        public OreSettings Get(OreType type)
		{
            return _ores.First(o => o.Type == type);
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
		}
    }
}
