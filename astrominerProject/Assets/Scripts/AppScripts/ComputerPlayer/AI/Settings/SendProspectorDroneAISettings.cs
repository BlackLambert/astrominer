using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SBaier.Astrominer
{
    
    [CreateAssetMenu(fileName = "SendProspectorDroneActionSettings", menuName = "ScriptableObjects/AIActions/SendProspectorDroneActionSettings")]
    public class SendProspectorDroneAISettings : ScriptableObject
    {
        [field: SerializeField] 
        public float BaseWeight { get; private set; } = 0;

        [field: SerializeField] 
        public float ActiveDronesWeightReductionFactor { get; private set; } = -50;

        [field: SerializeField] 
        public float ProspectingValueFactor { get; private set; } = 1;
        
        [field: SerializeField] 
        public AnimationCurve MoneyFactorCurve { get; private set; } = new AnimationCurve();

        [field: SerializeField] 
        public float MoneyFactor { get; private set; } = 5;

        [field: SerializeField] 
        public AIProspectingSettings ProspectingSettings { get; private set; }
    }
}
