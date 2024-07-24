using UnityEngine;

namespace SBaier.Astrominer
{
    [CreateAssetMenu(fileName = "SellOresAISettings", menuName = "ScriptableObjects/AIActions/SellOresAISettings")]
    public class SellOresAISettings : ScriptableObject
    {
        [SerializeField] private float _baseWeight = 0;
        public float BaseWeight => _baseWeight;

        [SerializeField] private AnimationCurve oresToSellValueFactorCurve;
        public AnimationCurve OresToSellValueFactorCurve => oresToSellValueFactorCurve;

        [SerializeField] private float oresToSellValueFactor;
        public float OresToSellValueFactor => oresToSellValueFactor;

        [SerializeField] private AnimationCurve _currentCreditsFactorCurve;
        public AnimationCurve CurrentCreditsFactorCurve => _currentCreditsFactorCurve;

        [SerializeField] private float _currentCreditsValueFactor;
        public float CurrentCreditsValueFactor => _currentCreditsValueFactor;

        [SerializeField] private AnimationCurve _distanceToBaseFactorCurve;
        public AnimationCurve DistanceToBaseFactorCurve => _distanceToBaseFactorCurve;

        [SerializeField] private float _distanceToBaseFactor;
        public float DistanceToBaseFactor => _distanceToBaseFactor;
        
        [field: SerializeField]
        public AIOresToSellSettings OresToSellFinderSettings { get; private set; }
    }
}