using UnityEngine;

namespace SBaier.Astrominer
{
    public class OptimalOresToSellFinder
    {
        private readonly Ship _ship;
        private readonly OreBank _oreBank;
        private readonly AIOresToSellSettings _oresToSellSettings;
                
        public OptimalOresToSellFinder(
            Ship ship,
            OreBank oreBank,
            AIOresToSellSettings oresToSellSettings)
        {
            _ship = ship;
            _oreBank = oreBank;
            _oresToSellSettings = oresToSellSettings;
        }

        public Ores Search()
        {
            return new Ores(
                GetSellAmountFor(OreType.Iron),
                GetSellAmountFor(OreType.Gold),
                GetSellAmountFor(OreType.Platinum));
        }

        private float GetSellAmountFor(OreType oreType)
        {
            float portion = 0;
            
            float pricePortion = _oreBank.GetPriceRangePortionFor(oreType);
            portion += _oresToSellSettings.PriceRangeFactorCurve.Evaluate(pricePortion) *
                      _oresToSellSettings.PriceRangeFactor;
            
            float currentCredits = _ship.Player.Credits.Amount;
            portion += _oresToSellSettings.CurrentCreditsFactorCurve.Evaluate(currentCredits) *
                      _oresToSellSettings.CurrentCreditsFactor;
            
            float oreAmount = _ship.CollectedOres[oreType].Amount;

            return Mathf.Clamp(oreAmount * portion, 0, _ship.CollectedOres[oreType].Amount);
        }
    }
}