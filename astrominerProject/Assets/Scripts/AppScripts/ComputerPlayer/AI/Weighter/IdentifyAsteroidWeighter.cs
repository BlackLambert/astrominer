using SBaier.AI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class IdentifyAsteroidWeighter : Weighter
    {
        private AIBrain _brain;
        private IdentifyAsteroidAISettings _settings;

        public IdentifyAsteroidWeighter(
            IdentifyAsteroidAISettings settings,
            AIBrain brain)
        {
            _settings = settings;
            _brain = brain;
        }

        public float GetWeight()
        {
            float weight = _settings.BaseWeight;
            
            weight +=
                _settings.IdentifiedEmptyAsteroidsValueFactorCurve.Evaluate(_brain.ValueOfEmptyIdentifiedAsteroids) *
                _settings.IdentifiedEmptyAsteroidsValueFactor;

            float bestValue = _brain.UnoccupiedAsteroidWithBestValue != null
                ? _brain.UnoccupiedAsteroidWithBestValue.Value
                : 0;
            weight +=
                _settings.BestIdentifiedEmptyAsteroidValueFactorCurve.Evaluate(bestValue) *
                _settings.BestIdentifiedEmptyAsteroidValueFactor;
            
            weight += _brain.ActiveProspectorDronesAmount * _settings.ActiveDronesFactor;

            Debug.Log($"Identify asteroid weight: {weight}");
            return weight;
        }
    }
}