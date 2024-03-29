using System;

namespace SBaier.Astrominer
{
    public class Currency
    {
        public float Amount { get; private set; }
        public event Action OnAmountChanged;

        public Currency() { }
        public Currency(float amount)
		{
            Amount = amount;
        }

        public void Add(float amount)
		{
            if (amount < 0)
                throw new ArgumentOutOfRangeException();
            Amount += amount;
            if(amount > 0)
                OnAmountChanged?.Invoke();
        }

        public float RequestAllowNegative(float amount)
        {
            if(amount < 0)
                throw new ArgumentOutOfRangeException();
            Amount -= amount;
            if (amount > 0)
                OnAmountChanged?.Invoke();
            return amount;
        }

        public float Request(float amount)
		{
            if (amount > Amount)
                throw new ArgumentOutOfRangeException();
            return RequestAllowNegative(amount);
        }

        public void Set(float amount)
		{
            if (amount < 0)
                throw new ArgumentOutOfRangeException();
            Amount = amount;
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
