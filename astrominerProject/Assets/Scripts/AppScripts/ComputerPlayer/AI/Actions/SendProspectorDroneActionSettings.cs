using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SBaier.Astrominer
{
    
    [CreateAssetMenu(fileName = "SendProspectorDroneActionSettings", menuName = "ScriptableObjects/AIActions/SendProspectorDroneActionSettings")]
    public class SendProspectorDroneActionSettings : ScriptableObject
    {
        
        [field: SerializeField] 
        public float BaseWeight { get; private set; } = 0;

        [field: SerializeField] 
        public float ActiveDronesWeightReductionFactor { get; private set; } = -50;

        [field: SerializeField] 
        public float NoEmptyIdentifiedAsteroidsWeightValue { get; private set; } = 100;

        [field: SerializeField] 
        public float IdentifiedAsteroidsValueWeightFactor { get; private set; } = -1;

        [field: SerializeField] 
        public float AsteroidDistanceMaxWeightValue { get; private set; } = 18;

        [field: SerializeField] 
        public float MiningAsteroidsValueWeightFactor { get; private set; } = -1;

        [field: SerializeField] 
        public Vector2 IdealDistanceRange { get; private set; } = new Vector2(6, 12);
    }
}
