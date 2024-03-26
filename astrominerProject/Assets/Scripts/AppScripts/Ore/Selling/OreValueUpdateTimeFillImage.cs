using SBaier.DI;
using UnityEngine;
using UnityEngine.UI;

namespace SBaier.Astrominer
{
    public class OreValueUpdateTimeFillImage : MonoBehaviour, Injectable
    {
        [SerializeField] 
        private Image _image;
        
        private GameTime _gameTime;
        private OresSettings _oresSettings;
        private OreValue _oreValue;
        
        public void Inject(Resolver resolver)
        {
            _gameTime = resolver.Resolve<GameTime>();
            _oresSettings = resolver.Resolve<OresSettings>();
            _oreValue = resolver.Resolve<OreValue>();
        }

        private void Update()
        {
            if (_gameTime.Paused.Value)
            {
                return;
            }

            float min = _oreValue.TimeForNextValueUpdate - _oresSettings.OreValueUpdateFrequency;
            float delta = _gameTime.Value - min;
            float percentage = delta / _oresSettings.OreValueUpdateFrequency;
            _image.fillAmount = percentage;
        }
    }
}
