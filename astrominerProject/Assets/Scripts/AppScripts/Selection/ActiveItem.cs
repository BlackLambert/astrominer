using System;

namespace SBaier.Astrominer
{
    public abstract class ActiveItem<T>
    {
        public delegate void ValueChanged(T formerValue, T newValue);
        
        public T Value
        {
            get => _value;
            set
            {
                T former = _value;
                _value = value;
                OnValueChanged?.Invoke(former, value);
            }
        }
        private T _value;

        public bool HasValue => Value != null;

        public event ValueChanged OnValueChanged;
    }
}
