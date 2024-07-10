using System;
using SBaier.AI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class SendProspectorDroneWeighter : Weighter
    {
        private readonly AIBrain _brain;
        private readonly SendProspectorDroneAISettings _settings;

        public SendProspectorDroneWeighter(
            AIBrain brain,
            SendProspectorDroneAISettings settings)
        {
            _brain = brain;
            _settings = settings;
        }

        public float GetWeight()
        {
            Asteroid mostValuableUnidentifiedAsteroid = _brain.GetBestProspectTargetFor(ProspectorVesselType.Drone);
            
            //Any interesting asteroid?
            if (mostValuableUnidentifiedAsteroid == null)
            {
                throw new InvalidOperationException("There is no asteroid to identify");
            }

            float weight = _settings.BaseWeight;

            // Active drones amount
            weight += _brain.ActiveProspectorDronesAmount * _settings.ActiveDronesWeightReductionFactor;

            // Prospecting Value
            weight += _brain.GetProspectValueOf(ProspectorVesselType.Drone, mostValuableUnidentifiedAsteroid) *
                      _settings.ProspectingValueFactor;
            
            // Credits amount
            weight += _settings.MoneyFactorCurve.Evaluate(_brain.Credits) * _settings.MoneyFactor;
            
            Debug.Log($"Send prospector drone weight: {weight}");
            return weight;
        }
    }
}