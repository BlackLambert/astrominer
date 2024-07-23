using System;
using SBaier.AI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class FlyToUnidentifiedAsteroidWeighter : Weighter
    {
        private AIBrain _brain;
        private FlyToUnidentifiedAsteroidAISettings _aiSettings;
        
        public FlyToUnidentifiedAsteroidWeighter(AIBrain brain,
            FlyToUnidentifiedAsteroidAISettings aiSettings)
        {
            _brain = brain;
            _aiSettings = aiSettings;
        }
        
        public float GetWeight()
        {
            Asteroid asteroid = _brain.GetBestProspectTargetFor(ProspectorVesselType.Ship);

            if (asteroid == null)
            {
                throw new InvalidOperationException("There is no asteroid to identify");
            }
            
            float weight = _aiSettings.BaseWeight;
            
            // Prospecting Value
            weight += _brain.GetProspectValueOf(ProspectorVesselType.Ship, asteroid) *
                      _aiSettings.ProspectingValueFactor;
            return weight;
        }
    }
}