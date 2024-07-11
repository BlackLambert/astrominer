using UnityEngine;

namespace SBaier.Astrominer
{
    [CreateAssetMenu(fileName = "AIProspectingSettings", menuName = "ScriptableObjects/AIActions/AIProspectingSettings")]
    public class AIProspectingSettings : ScriptableObject
    {
        [field: SerializeField] 
        public AnimationCurve DistanceFactorCurve { get; private set; } = new AnimationCurve();

        [field: SerializeField] 
        public float DistanceValueFactor { get; private set; } = 3;        
        
        [field: SerializeField] 
        public AnimationCurve DistanceToBaseFactorCurve { get; private set; } = new AnimationCurve();

        [field: SerializeField] 
        public float DistanceToBaseValueFactor { get; private set; } = 3;
        
        [field: SerializeField] 
        public float SizeValueFactor { get; private set; } = 0.33f;
    }
}