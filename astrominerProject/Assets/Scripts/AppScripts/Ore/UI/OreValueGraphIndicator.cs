using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class OreValueGraphIndicator : MonoBehaviour, Injectable
    {
        [SerializeField] 
        private RectTransform _indicator;
        
        private OreType _oreType;
        private OreValue _oreValue;
        private OresSettings.OreSettings _oreSettings;
        private float _valueDelta;
        
        public void Inject(Resolver resolver)
        {
            _oreType = resolver.Resolve<OreType>();
            _oreValue = resolver.Resolve<OreValue>();
            _oreSettings = resolver.Resolve<OresSettings>().Get(_oreType);
        }

        private void OnEnable()
        {
            _valueDelta = _oreSettings.PriceRange.y - _oreSettings.PriceRange.x;
            UpdatePosition(_oreValue.GetValue(_oreType));
            _oreValue.OnValueChanged += OnOreValueChanged;
        }

        private void OnDisable()
        {
            _oreValue.OnValueChanged -= OnOreValueChanged;
        }

        private void OnOreValueChanged(OreType oreType, float value)
        {
            if (oreType != _oreType)
            {
                return;
            }
            
            UpdatePosition(value);
        }

        private void UpdatePosition(float value)
        {
            float percentage = (value - _oreSettings.PriceRange.x) / _valueDelta;
            _indicator.anchorMin = new Vector2(0, percentage);
            _indicator.anchorMax = new Vector2(0, percentage);
            _indicator.anchoredPosition = Vector2.zero;
        }
    }
}
