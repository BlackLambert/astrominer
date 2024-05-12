using System.Collections.Generic;
using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class PlayerValueMinMaxUpdater : MonoBehaviour, Injectable, Initializable, Cleanable
    {
        private PlayerValues _playerValues;
        
        public void Inject(Resolver resolver)
        {
            _playerValues = resolver.Resolve<PlayerValues>();
        }

        public void Initialize()
        {
            foreach (KeyValuePair<Player, PlayerValue> pair in _playerValues.Values)
            {
                pair.Value.TotalValue.OnValueChanged += OnValueChanged;
            }

            InitMinMax();
        }

        public void Clean()
        {
            foreach (KeyValuePair<Player, PlayerValue> pair in _playerValues.Values)
            {
                pair.Value.TotalValue.OnValueChanged -= OnValueChanged;
            }
        }

        private void InitMinMax()
        {
            foreach (KeyValuePair<Player, PlayerValue> pair in _playerValues.Values)
            {
                foreach (float value in pair.Value.ValueHistory)
                {
                    UpdateMinMax(value);
                }
            }
        }

        private void OnValueChanged(float formervalue, float newvalue)
        {
            UpdateMinMax(newvalue);
        }

        private void UpdateMinMax(float value)
        {
            if (value > _playerValues.MinMax.Max)
            {
                _playerValues.MinMax = new MinMax() { Min = _playerValues.MinMax.Min, Max = value };
            } 
            else if (value < _playerValues.MinMax.Min)
            {
                _playerValues.MinMax = new MinMax() { Min = value, Max = _playerValues.MinMax.Max };
            }
        }
    }
}
