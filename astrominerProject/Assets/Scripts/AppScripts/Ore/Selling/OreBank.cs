using SBaier.DI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class OreBank : Injectable
    {
        private OresSellingSettings _sellingSettings;

        public void Inject(Resolver resolver)
        {
            _sellingSettings = resolver.Resolve<OresSellingSettings>();
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
            float creditsPerOre = _sellingSettings.Get(oreType).Price;
            float creditsAmount = creditsPerOre * amount;
            return Mathf.Max(0, creditsAmount);
        }
    }
}
