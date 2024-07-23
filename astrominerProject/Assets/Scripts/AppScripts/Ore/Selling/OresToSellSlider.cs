using System;
using SBaier.DI;
using UnityEngine;
using UnityEngine.UI;

namespace SBaier.Astrominer
{
    public class OresToSellSlider : MonoBehaviour, Injectable, Initializable, Cleanable
    {
        [SerializeField] private Slider _slider;

        private OresToSell _oresToSell;
        private Ship _ship;
        private OreType _oreType;

        private void Reset()
        {
            _slider = GetComponent<Slider>();
        }

        public void Inject(Resolver resolver)
        {
            _oresToSell = resolver.Resolve<OresToSell>();
            _ship = resolver.Resolve<Ship>();
            _oreType = resolver.Resolve<OreType>();
        }

        public void Initialize()
        {
            InitSlider();
            _oresToSell.Ores.OnValueChanged += UpdateSliderValue;
            _ship.CollectedOres.OnValueChanged += InitSlider;
            _slider.onValueChanged.AddListener(OnValueChanged);
        }

        public void Clean()
        {
            _oresToSell.Ores.OnValueChanged -= UpdateSliderValue;
            _ship.CollectedOres.OnValueChanged -= InitSlider;
            _slider.onValueChanged.RemoveListener(OnValueChanged);
        }

        private void InitSlider()
        {
            _slider.minValue = 0;
            _slider.maxValue = _ship.CollectedOres[_oreType].Amount;
            UpdateSliderValue();
        }

        private void UpdateSliderValue()
        {
            _slider.SetValueWithoutNotify(_oresToSell.Ores[_oreType].Amount);
        }

        private void OnValueChanged(float value)
        {
            value = Mathf.Clamp(value, 0, _ship.CollectedOres[_oreType].Amount);
            _oresToSell.Ores.ChangeTo(_oreType, value);
        }
    }
}