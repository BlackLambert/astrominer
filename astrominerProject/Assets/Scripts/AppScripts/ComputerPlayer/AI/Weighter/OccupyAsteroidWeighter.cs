using System;
using SBaier.AI;
using UnityEngine;

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
            Asteroid occupationTarget = _brain.UnoccupiedAsteroidWithBestValue;

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
            
            // Amount of exploit machines in inventory
            int exploitersAmount = _brain.ExploitersInInventoryAmount;
            weight += _aiSettings.ExploiterAmountValueCurve.Evaluate(exploitersAmount) * 
                      _aiSettings.ExploiterAmountFactor;
            
            Debug.Log($"Occupy asteroid weight: {weight}");
            return weight;
        }
    }
}