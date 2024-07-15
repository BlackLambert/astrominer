using UnityEngine;

namespace SBaier.Astrominer
{
    [CreateAssetMenu(fileName = "EarnCreditsAiSettings", menuName = "ScriptableObjects/AIActions/EarnCreditsAiSettings")]
    public class EarnCreditsAiSettings : ScriptableObject
    {
        [SerializeField] private DroneSettings _carrierDroneSettings;
        public DroneSettings CarrierDroneSettings => _carrierDroneSettings;
        
        [SerializeField] private float _baseWeight = 0;
        public float BaseWeight => _baseWeight;

        [SerializeField] private AnimationCurve _storedOresValueWeightFactorCurve;
        public AnimationCurve StoredOresValueWeightFactorCurve => _storedOresValueWeightFactorCurve;

        [SerializeField] private float _storedOresValueWeightFactor;
        public float StoredOresValueWeightFactor => _storedOresValueWeightFactor;

        [SerializeField] private AnimationCurve _creditsWeightFactorCurve;
        public AnimationCurve CreditsWeightFactorCurve => _creditsWeightFactorCurve;

        [SerializeField] private float _creditsWeightFactor;
        public float CreditsWeightFactor => _creditsWeightFactor;

        [SerializeField] private AnimationCurve _pendingCreditsWeightFactorCurve;
        public AnimationCurve PendingCreditsWeightFactorCurve => _pendingCreditsWeightFactorCurve;

        [SerializeField] private float _pendingCreditsWeightFactor;
        public float PendingCreditsWeightFactor => _pendingCreditsWeightFactor;
        
    }
}