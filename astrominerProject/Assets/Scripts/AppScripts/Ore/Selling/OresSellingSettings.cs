using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

namespace SBaier.Astrominer
{
    [CreateAssetMenu(fileName = "OreSellingSettings", menuName = "ScriptableObjects/OreSellingSettings")]
    public class OresSellingSettings : ScriptableObject
    {
        [field: SerializeField]
        private List<OreSellingSettings> _sellingSettings = new List<OreSellingSettings>();

        public OreSellingSettings Get(OreType type)
        {
            OreSellingSettings result = _sellingSettings.FirstOrDefault(s => s.Type == type);
            if (result == null)
                throw new NotImplementedException($"There is no selling setting for the ore of type {type}");
            return result;
        }

        [Serializable]
        public class OreSellingSettings
        {
            [field: SerializeField]
            public OreType Type { get; private set; }
            [field: SerializeField]
            public float Price { get; private set; }
        }
    }
}
