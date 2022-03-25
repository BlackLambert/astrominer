using System;

namespace SBaier.Astrominer
{
    public class Currency
    {
        public float Amount { get; private set; }
        public event Action OnAmountChanged;

        public void Add(float amount)
		{
            if (amount < 0)
                throw new ArgumentOutOfRangeException();
            Amount += amount;
            OnAmountChanged?.Invoke();
        }

        public void Request(float amount)
		{
            if (amount > Amount)
                throw new ArgumentOutOfRangeException();
            Amount -= amount;
            OnAmountChanged?.Invoke();
        }

        public bool Has(float amount)
		{
            return Amount >= amount;
        }

		public override string ToString()
		{
            return Amount.ToString("N0");
		}
	}
}
