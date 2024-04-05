using System;
using SBaier.DI;
using UnityEngine;
using UnityEngine.Serialization;

namespace SBaier.Astrominer
{
    public class FloatSliderInput : SliderInput<float>
    {
        [FormerlySerializedAs("_amountOfDecimalPlacesDisplayed")]
        [SerializeField] 
        [Range(0, 10)] 
        private int _decimalPlacesDisplayed = 1;
        [SerializeField] 
        [Range(0, 10)] 
        private int _roundToDecimalPlaces = 1;

        [SerializeField] 
        private bool _percentage = false;
        
        [SerializeField] 
        private bool _wholeNumbers = false;
        
        private Arguments _arguments;

        protected override bool WholeNumbers => _wholeNumbers;

        public override void Inject(Resolver resolver)
        {
            base.Inject(resolver);
            _arguments = resolver.Resolve<Arguments>();
        }

        protected override float ToValue(float value)
        {
            return (float) Math.Round(value, _roundToDecimalPlaces);
        }

        protected override float ToFloat(float value)
        {
            return (float) Math.Round(value, _roundToDecimalPlaces);
        }

        protected override float GetMin()
        {
            return _arguments.Range.x;
        }

        protected override float GetMax()
        {
            return _arguments.Range.y;
        }

        protected override string ToString(float value)
        {
            if (_percentage)
            {
                string valueString = (value * 100).ToString($"F{_decimalPlacesDisplayed}");
                return $"{valueString}%";
            }
            return value.ToString($"F{_decimalPlacesDisplayed}");
        }

        public class Arguments
        {
            public Vector2 Range;
        }
    }
}
