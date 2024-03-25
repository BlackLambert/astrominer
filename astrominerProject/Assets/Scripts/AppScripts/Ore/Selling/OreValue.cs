using System;
using System.Collections.Generic;

namespace SBaier.Astrominer
{
    public class OreValue
    {
        public event Action<OreType, float> OnValueChanged;
        
        private Dictionary<OreType, float> _oreToValue = new Dictionary<OreType, float>();

        public OreValue()
        {
            FillData();
        }

        public float Get(OreType type)
        {
            if (!_oreToValue.TryGetValue(type, out float result))
            {
                throw new ArgumentException($"There is no value data for ore {type.ToString()}.");
            }

            return result;
        }

        public void Set(OreType type, float value)
        {
            _oreToValue[type] = value;
            OnValueChanged?.Invoke(type, value);
        }

        private void FillData()
        {
            foreach (OreType oreType in Enum.GetValues(typeof(OreType)))
            {
                _oreToValue.Add(oreType, 0);
            }
        }
    }
}