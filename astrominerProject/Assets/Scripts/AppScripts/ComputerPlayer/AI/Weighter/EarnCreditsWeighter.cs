using SBaier.AI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class EarnCreditsWeighter : Weighter
    {
        private readonly EarnCreditsAiSettings _aiSettings;
        private readonly AIBrain _brain;

        public EarnCreditsWeighter(EarnCreditsAiSettings aiSettings, AIBrain brain)
        {
            _aiSettings = aiSettings;
            _brain = brain;
        }
        
        public float GetWeight()
        {
            // Amount of stored ores in asteroids
            float valueOfStoredOresByAsteroids = _brain.GetValueOfStoredAsteroidOres();
            
            // Amount of stored ores in ship
            float valueOfStoredOresOnShip = _brain.GetValueOfStoredShipOres();
            float storedOresValueSum = valueOfStoredOresByAsteroids + valueOfStoredOresOnShip;
            float storedOresValueFactor = _aiSettings.StoredOresValueWeightFactorCurve.Evaluate(storedOresValueSum);
            float storedOresWeight = storedOresValueFactor *
                                     _aiSettings.StoredOresValueWeightFactor;
            
            // Current credits
            float credits = _brain.Credits;
            
            // Amount of "pending" credits => active carrier drones
            float pendingCredits = _brain.GetPendingCredits();
            float allCredits = credits + pendingCredits;
            float allCreditsWeight = _aiSettings.CreditsWeightFactorCurve.Evaluate(allCredits) *
                                     _aiSettings.CreditsWeightFactor;
            float pendingCreditsWeight = _aiSettings.PendingCreditsWeightFactorCurve.Evaluate(pendingCredits) *
                                     _aiSettings.PendingCreditsWeightFactor;

            float weight = _aiSettings.BaseWeight + storedOresWeight + allCreditsWeight * storedOresValueFactor + pendingCreditsWeight;
            Debug.Log($"Earn credits weight: {weight}");
            return weight;
        }
    }
}