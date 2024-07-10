using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class OreBank : Injectable
    {
        private OreValue _oreValue;

        public void Inject(Resolver resolver)
        {
            _oreValue = resolver.Resolve<OreValue>();
        }

        public float CalculateCreditsFor(Ores ores)
        {
            float creditsAmount = 0;
            foreach (OreType oreType in ores.OreTypes)
                creditsAmount += CalculateCreditsFor(oreType, ores[oreType].Amount);
            return creditsAmount;
        }

        public float CalculateCreditsFor(OreType oreType, float amount)
        {
            float creditsPerOre = _oreValue.GetValue(oreType);
            float creditsAmount = creditsPerOre * amount;
            return Mathf.Max(0, creditsAmount);
        }
    }
}
