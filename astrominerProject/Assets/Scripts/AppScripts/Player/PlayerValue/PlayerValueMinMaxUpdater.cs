using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class PlayerValueMinMaxUpdater : MonoBehaviour, Injectable, Initializable, Cleanable
    {
        private PlayerValue _playerValue;
        private PlayerValues _playerValues;
        private PlayerValueSettings _settings;
        
        public void Inject(Resolver resolver)
        {
            _playerValue = resolver.Resolve<PlayerValue>();
            _playerValues = resolver.Resolve<PlayerValues>();
            _settings = resolver.Resolve<PlayerValueSettings>();
        }

        public void Initialize()
        {
            _playerValue.TotalValue.OnValueChanged += OnValueChanged;
            _playerValue.OnReset += UpdateMinMax;
            UpdateMinMax();
        }

        public void Clean()
        {
            _playerValue.TotalValue.OnValueChanged -= OnValueChanged;
            _playerValue.OnReset -= UpdateMinMax;
        }

        private void OnValueChanged(float formervalue, float newvalue)
        {
            UpdateMinMax();
        }

        private void UpdateMinMax()
        {
            _playerValues.MinMax = new MinMax
            {
                Min = _playerValue.TotalValue + _settings.ValueOffset.Min, 
                Max = _playerValue.TotalValue + _settings.ValueOffset.Max
            };
            
            foreach (PlayerValue values in _playerValues.Values.Values)
            {
                if (values.ValueHistory.Count <= 0)
                {
                    continue;
                }
                
                foreach (float value in values.ValueHistory.GetLastXElements(_playerValue.ValueHistory.Count))
                {
                    UpdateMinMax(value);
                }
            }
        }

        private void UpdateMinMax(float value)
        {
            if (value > _playerValues.MinMax.Max - _settings.ValueOffset.Max)
            {
                _playerValues.MinMax = new MinMax() 
                { 
                    Min = _playerValues.MinMax.Min,
                    Max = value  + _settings.ValueOffset.Max
                };
            } 
            else if (value < _playerValues.MinMax.Min - _settings.ValueOffset.Min)
            {
                _playerValues.MinMax = new MinMax()
                {
                    Min = value + _settings.ValueOffset.Min, 
                    Max = _playerValues.MinMax.Max
                };
            }
        }
    }
}
