using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class IntSliderInput : SliderInput<int>
    {
        private Arguments _arguments;

        protected override bool WholeNumbers => true;

        public override void Inject(Resolver resolver)
        {
            base.Inject(resolver);
            _arguments = resolver.Resolve<Arguments>();
        }

        protected override int ToValue(float value)
        {
            return Mathf.RoundToInt(value);
        }

        protected override float ToFloat(int value)
        {
            return value;
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
            return value.ToString("F0");
        }

        public class Arguments
        {
            public Vector2Int Range;
        }
    }
}
