using System;
using SBaier.DI;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

namespace SBaier.Astrominer
{
    public abstract class SliderInput<T> : MonoBehaviour, Injectable, Initializable, Cleanable
    {
        [SerializeField] 
        private TextMeshProUGUI _currentAmountText;
        [SerializeField] 
        private TextMeshProUGUI _minAmountText;
        [SerializeField] 
        private TextMeshProUGUI _maxAmountText;
        [SerializeField] 
        private Slider _slider;

        protected abstract bool WholeNumbers { get; }
        private Observable<T> _value;

        public virtual void Inject(Resolver resolver)
        {
            _value = resolver.Resolve<Observable<T>>();
        }

        public void Initialize()
        {
            InitSlider();
            _value.OnValueChanged += UpdateInput;
            _slider.onValueChanged.AddListener(OnSliderValueChanged);
        }

        public void Clean()
        {
            _value.OnValueChanged -= UpdateInput;
            _slider.onValueChanged.RemoveListener(OnSliderValueChanged);
        }

        private void InitSlider()
        {
            float min = GetMin();
            float max = GetMax();
            _slider.wholeNumbers = WholeNumbers;
            _slider.minValue = min;
            _slider.maxValue = max;
            _slider.value = ToFloat(_value.Value);
            _minAmountText.text = ToString(min);
            _maxAmountText.text = ToString(max);
            _currentAmountText.text = ToString(ToFloat(_value.Value));
        }

        private void OnSliderValueChanged(float value)
        {
            _value.Value = ToValue(value);
        }

        private void UpdateInput(T formervalue, T newvalue)
        {
            float floatValue = ToFloat(newvalue);
            if (floatValue > _slider.maxValue || floatValue < _slider.minValue)
            {
                throw new ArgumentException("Please make sure the value fits the slider borders");
            }
            
            _slider.SetValueWithoutNotify(floatValue);
            _currentAmountText.text = ToString(floatValue);
        }

        protected abstract T ToValue(float value);
        protected abstract float ToFloat(T value);
        protected abstract float GetMin();
        protected abstract float GetMax();
        protected abstract string ToString(float value);
    }
}
