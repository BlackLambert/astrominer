using System;
using SBaier.DI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SBaier.Astrominer
{
    public class IntSliderInput : MonoBehaviour, Injectable
    {
        [SerializeField] 
        private TextMeshProUGUI _currentAmountText;
        [SerializeField] 
        private TextMeshProUGUI _minAmountText;
        [SerializeField] 
        private TextMeshProUGUI _maxAmountText;
        [SerializeField] 
        private Slider _slider;

        private Observable<int> _value;
        private Arguments _arguments;

        public void Inject(Resolver resolver)
        {
            _value = resolver.Resolve<Observable<int>>();
            _arguments = resolver.Resolve<Arguments>();
        }

        private void OnEnable()
        {
            InitSlider();
            _value.OnValueChanged += UpdateInput;
            _slider.onValueChanged.AddListener(OnSliderValueChanged);
        }

        private void OnDisable()
        {
            _value.OnValueChanged -= UpdateInput;
            _slider.onValueChanged.RemoveListener(OnSliderValueChanged);
        }

        private void InitSlider()
        {
            float min = _arguments.Range.x;
            float max = _arguments.Range.y;
            _slider.wholeNumbers = true;
            _slider.minValue = _arguments.Range.x;
            _slider.maxValue = _arguments.Range.y;
            _slider.value = _value.Value;
            _minAmountText.text = min.ToString();
            _maxAmountText.text = max.ToString();
            _currentAmountText.text = _value.Value.ToString();
        }

        private void OnSliderValueChanged(float value)
        {
            _value.Value = Mathf.RoundToInt(value);
        }

        private void UpdateInput(int formervalue, int newvalue)
        {
            if (newvalue > _slider.maxValue || newvalue < _slider.minValue)
            {
                throw new ArgumentException("Please make sure the value fits the slider borders");
            }
            
            _slider.SetValueWithoutNotify(newvalue);
            _currentAmountText.text = newvalue.ToString();
        }

        public class Arguments
        {
            public Vector2Int Range;
        }
    }
}
