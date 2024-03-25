using SBaier.DI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class OreBank : Injectable
    {
        private OresSettings _oreSettings;

        public void Inject(Resolver resolver)
        {
            _oreSettings = resolver.Resolve<OresSettings>();
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
            float creditsPerOre = _oreSettings.Get(oreType).PriceRange.x;
            float creditsAmount = creditsPerOre * amount;
            return Mathf.Max(0, creditsAmount);
        }
    }
}
