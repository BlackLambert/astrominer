using SBaier.AI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class SellOresWeighter : Weighter
    {
        private readonly SellOresAISettings _aiSettings;
        private readonly AIBrain _brain;

        public SellOresWeighter(SellOresAISettings aiSettings, AIBrain brain)
        {
            _aiSettings = aiSettings;
            _brain = brain;
        }

        public float GetWeight()
        {
            float weight = _aiSettings.BaseWeight;

            // Value of ores to sell
            float valueOfOresToSell = _brain.GetValueOfOresToSell();
            weight += _aiSettings.OresToSellValueFactorCurve.Evaluate(valueOfOresToSell) *
                      _aiSettings.OresToSellValueFactor;
            
            // Current credits
            float credits = _brain.GetAllCredits();
            weight += _aiSettings.CurrentCreditsFactorCurve.Evaluate(credits) *
                      _aiSettings.CurrentCreditsValueFactor;
            
            // Distance to BaseFactor
            float distanceToBase = _brain.GetDistanceToBase();
            weight += _aiSettings.DistanceToBaseFactorCurve.Evaluate(distanceToBase) *
                      _aiSettings.DistanceToBaseFactor;
            
            return weight;
        }
    }
}