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

            // Value of stored ship ores
            float valueOfOresStoredInShip = _brain.GetValueOfStoredShipOres();
            weight += _aiSettings.StoredShipOresValueFactorCurve.Evaluate(valueOfOresStoredInShip) *
                      _aiSettings.StoredShipOresValueFactor;
            
            // Distance to BaseFactor
            float distanceToBase = _brain.GetDistanceToBase();
            weight += _aiSettings.DistanceToBaseFactorCurve.Evaluate(distanceToBase) *
                      _aiSettings.DistanceToBaseFactor;
            return weight;
        }
    }
}