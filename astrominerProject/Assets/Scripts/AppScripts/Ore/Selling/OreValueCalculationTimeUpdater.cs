using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class OreValueCalculationTimeUpdater : MonoBehaviour, Injectable
    {
        private OreValue _oreValue;
        private GameTime _gameTime;
        private OresSettings _oresSettings;
        
        public void Inject(Resolver resolver)
        {
            _oreValue = resolver.Resolve<OreValue>();
            _gameTime = resolver.Resolve<GameTime>();
            _oresSettings = resolver.Resolve<OresSettings>();
        }

        private void Update()
        {
            CheckUpdateTime();
        }

        private void CheckUpdateTime()
        {
            if (_gameTime.Paused || _gameTime.Value < _oreValue.TimeForNextValueUpdate)
            {
                return;
            }

            while (_gameTime.Value >= _oreValue.TimeForNextValueUpdate)
            {
                _oreValue.TimeForNextValueUpdate.Value += _oresSettings.OreValueUpdateFrequency;
                _oreValue.TriggerShallUpdateValue();
            }
        }
    }
}
