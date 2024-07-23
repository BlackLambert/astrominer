using SBaier.AI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class CollectOresWeighter : Weighter
    {
        private readonly CollectOresAISettings _aiSettings;
        private readonly AIBrain _brain;

        public CollectOresWeighter(CollectOresAISettings aiSettings, AIBrain brain)
        {
            _aiSettings = aiSettings;
            _brain = brain;
        }
        
        public float GetWeight()
        {
            float weight = _aiSettings.BaseWeight;
            
            // Value of best collect ores target
            weight += _brain.GetCollectOreValueOf(CollectOresVesselType.Ship,
                _brain.GetBestCollectTargetFor(CollectOresVesselType.Ship));
            return weight;
        }
    }
}