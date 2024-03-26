using System;
using System.Collections.Generic;

namespace SBaier.Astrominer
{
    public class OreValue
    {
        public event Action<OreType, float> OnValueChanged;
        public event Action OnShallUpdateValue;
        
        private Dictionary<OreType, float> _oreToValue = new Dictionary<OreType, float>();

        private Dictionary<OreType, CircularBuffer<float>> _oreToFormerValue =
            new Dictionary<OreType, CircularBuffer<float>>();

        public Observable<float> TimeForNextValueUpdate { get; private set; } = 0;

        public OreValue(int formerValueBufferSize = 100)
        {
            FillData(formerValueBufferSize);
        }

        public float GetValue(OreType type)
        {
            if (!_oreToValue.TryGetValue(type, out float result))
            {
                throw new ArgumentException($"There is no value data for ore {type.ToString()}.");
            }

            return result;
        }

        public CircularBuffer<float> GetFormerValue(OreType type)
        {
            if (!_oreToFormerValue.TryGetValue(type, out CircularBuffer<float> result))
            {
                throw new ArgumentException($"There is no former value data for ore {type.ToString()}.");
            }

            return result;
        }

        public void SetCurrentValue(OreType type, float value)
        {
            _oreToValue[type] = value;
            OnValueChanged?.Invoke(type, value);
        }

        public void TriggerShallUpdateValue()
        {
            OnShallUpdateValue?.Invoke();
        }

        private void FillData(int formerValueBufferSize)
        {
            foreach (OreType oreType in Enum.GetValues(typeof(OreType)))
            {
                if (oreType == OreType.None)
                {
                    continue;
                }
                
                _oreToValue.Add(oreType, 0);
                _oreToFormerValue.Add(oreType, new CircularBuffer<float>(formerValueBufferSize));
            }
        }
    }
}