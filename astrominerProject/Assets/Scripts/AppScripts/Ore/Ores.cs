using System;
using System.Collections.Generic;
using System.Linq;

namespace SBaier.Astrominer
{
    public class Ores
    {
        private Dictionary<OreType, Currency> _ores = new Dictionary<OreType, Currency>();
        public List<OreType> OreTypes { get; }

        public event Action OnValueChanged;

        public Ores() : this(0, 0, 0)
        {

        }

        public Ores(Ores ores) : this(ores[OreType.Iron].Amount,
            ores[OreType.Gold].Amount,
            ores[OreType.Platinum].Amount)
        {
           
        }

        public Ores(float iron, float gold, float platinum)
		{
            _ores[OreType.Iron] = new Currency(iron);
            _ores[OreType.Gold] = new Currency(gold);
            _ores[OreType.Platinum] = new Currency(platinum);
            OreTypes = GetOreTypes();
        }

        public void Add(Ores ores)
		{
            foreach (OreType oreType in ores.GetOreTypes())
                AddInternal(oreType, ores[oreType].Amount);
            OnValueChanged?.Invoke();
        }

        public void Add(OreType type, float amount)
        {
            AddInternal(type, amount);
            OnValueChanged?.Invoke();
        }

        private void AddInternal(OreType type, float amount)
		{
            _ores[type].Add(amount);
        }

        public Ores Request(Ores ores)
		{
            Ores result = new Ores();
			foreach (OreType type in ores.GetOreTypes())
                result[type].Add(RequestInternal(type, ores[type].Amount));
            OnValueChanged?.Invoke();
            return result;
        }

        public float Request(OreType type, float amount)
		{
            float result = RequestInternal(type, amount);
            OnValueChanged?.Invoke();
            return result;
		}

        public Ores RequestAll()
		{
            return Request(this);
		}

        private float RequestInternal(OreType type, float amount)
		{
            return _ores[type].Request(amount);
        }

        public float GetTotal()
		{
            return _ores.Values.Sum(v => v.Amount);
        }

        public bool IsEmpty()
		{
            return GetTotal() <= 0;
        }

        public Currency this[OreType type]
        {
            get => _ores[type];
        }

        private List<OreType> GetOreTypes()
		{
            return _ores.Keys.ToList();
		}
    }
}
