using System;
using SBaier.AI;

namespace SBaier.Astrominer
{
    public class OccupyAsteroidWeighter : Weighter
    {
        private readonly OccupyAsteroidAiSettings _aiSettings;
        private readonly AIBrain _brain;

        public OccupyAsteroidWeighter(
            OccupyAsteroidAiSettings aiSettings,
            AIBrain brain)
        {
            _aiSettings = aiSettings;
            _brain = brain;
        }
        
        public float GetWeight()
        {
            Asteroid occupationTarget = _brain.GetUnoccupiedAsteroidWithBestValue();

            if (occupationTarget == null)
            {
                throw new InvalidOperationException("There is no asteroid to occupy");
            }

            float weight = _aiSettings.BaseWeight;

            // Value of best identified unoccupied asteroid
            float occupationValue = _brain.GetOccupationValueOf(occupationTarget);
            weight += occupationValue * _aiSettings.OccupationValueFactor;
            
            // Amount of occupation targets
            int occupationTargetsAmount = _brain.OccupationTargets.Count;
            weight += _aiSettings.OccupationTargetsAmountValueCurve.Evaluate(occupationTargetsAmount) * 
                      _aiSettings.OccupationTargetsAmountFactor;
            
            return weight;
        }
    }
}