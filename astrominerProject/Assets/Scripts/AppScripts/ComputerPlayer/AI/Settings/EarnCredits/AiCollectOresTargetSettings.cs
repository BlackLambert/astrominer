using UnityEngine;

namespace SBaier.Astrominer
{
    [CreateAssetMenu(fileName = "AiCollectOresTargetSettings", menuName = "ScriptableObjects/AIActions/AiCollectOresTargetSettings")]
    public class AiCollectOresTargetSettings : ScriptableObject
    {
        [SerializeField] 
        private AnimationCurve _distanceFactorCurve;
        public AnimationCurve DistanceFactorCurve => _distanceFactorCurve;

        [SerializeField]
        private float _distanceValueFactor;
        public float DistanceValueFactor => _distanceValueFactor;

        [SerializeField] 
        private AnimationCurve oreValueFactorCurve;
        public AnimationCurve OreValueFactorCurve => oreValueFactorCurve;
        
        [SerializeField] 
        private float _valueFactor;
        public float ValueFactor => _valueFactor;
    }
}