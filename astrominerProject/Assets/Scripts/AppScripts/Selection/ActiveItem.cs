using System;

namespace SBaier.Astrominer
{
    public abstract class ActiveItem<T>
    {
        public T Value
        {
            get => _value;
            set
            {
                _value = value;
                OnValueChanged?.Invoke();
            }
        }
        private T _value;

        public bool HasValue => Value != null;

        public event Action OnValueChanged;
    }
}
