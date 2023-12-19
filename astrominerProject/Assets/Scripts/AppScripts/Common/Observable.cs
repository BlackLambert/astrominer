namespace SBaier.Astrominer
{
    public class Observable<T>
    {
        public delegate void ValueChanged(T formerValue, T newValue);
            
        public event ValueChanged OnValueChanged;

        public T Value
        {
            get => _value;
            set
            {
                T former = _value;
                _value = value;
                OnValueChanged?.Invoke(former, _value);
            }
        }

        private T _value;
        
        public static implicit operator Observable<T>(T value) => new(){_value = value};
        public static implicit operator T(Observable<T> value) => value.Value;
    }
}
