using SBaier.AI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class SendCarrierDroneWeighter : Weighter
    {
        private readonly SendCarrierDroneAISettings _aiSettings;
        private readonly AIBrain _brain;
        
        public SendCarrierDroneWeighter(SendCarrierDroneAISettings aiSettings, AIBrain brain)
        {
            _aiSettings = aiSettings;
            _brain = brain;
        }
        
        public float GetWeight()
        {
            float weight = _aiSettings.BaseWeight;
            
            // Value of best collect ores target
            weight += _brain.GetCollectOreValueOf(CollectOresVesselType.Drone,
                _brain.GetBestCollectTargetFor(CollectOresVesselType.Drone)) * _aiSettings.CollectValueFactor;
            
            // Current credits
            weight += _aiSettings.CurrentCreditsFactorCurve.Evaluate(_brain.Credits) *
                      _aiSettings.CurrentCreditsFactor;
            
            return weight;
        }
    }
}