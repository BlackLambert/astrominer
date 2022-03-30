using System;

namespace SBaier.Astrominer
{
    public class Ores
    {
        public Currency Iron { get; } = new Currency();
        public Currency Gold { get; } = new Currency();
        public Currency Platinum { get; } = new Currency();

        public event Action OnValueChanged;

        public Ores() { }

        public Ores(float iron, float gold, float platinum)
		{
            Add(iron, gold, platinum);
        }

        public void Add(Ores ores)
		{
            Add(ores.Iron.Amount, ores.Gold.Amount, ores.Platinum.Amount);
        }

        public void Add(float iron, float gold, float platinum)
        {
            Iron.Add(iron);
            Gold.Add(gold);
            Platinum.Add(platinum);
            OnValueChanged?.Invoke();
        }

        public void Request(float iron, float gold, float platinum)
		{
            Iron.Request(iron);
            Gold.Request(gold);
            Platinum.Request(platinum);
            OnValueChanged?.Invoke();
        }

        public float GetTotal()
		{
            float result = 0;
            result += Iron.Amount;
            result += Gold.Amount;
            result += Platinum.Amount;
            return result;
        }

        public bool IsEmpty()
		{
            return Iron.Amount <= 0 && Gold.Amount <= 0 && Platinum.Amount <= 0;
        }
    }
}
