using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class OreBank : Injectable
    {
        private OreValue _oreValue;
        private OresSettings _oresSettings;

        public void Inject(Resolver resolver)
        {
            _oreValue = resolver.Resolve<OreValue>();
            _oresSettings = resolver.Resolve<OresSettings>();
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

        public float GetPriceRangePortionFor(OreType oreType)
        {
            float creditsPerOre = _oreValue.GetValue(oreType);
            OresSettings.OreSettings oreSettings = _oresSettings.Get(oreType);
            float delta = oreSettings.PriceRange.y - oreSettings.PriceRange.x;
            return (creditsPerOre - oreSettings.PriceRange.x) / delta;
        }
    }
}
