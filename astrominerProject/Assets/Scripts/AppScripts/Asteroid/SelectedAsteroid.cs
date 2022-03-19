using System;

namespace SBaier.Astrominer
{
    public class SelectedAsteroid
    {
        public Asteroid Value 
        {
            get => _value;
            set
			{
                _value = value;
                OnValueChanged?.Invoke();
            }
        }
        private Asteroid _value;

        public bool HasValue => Value != null;

        public event Action OnValueChanged;
    }
}
