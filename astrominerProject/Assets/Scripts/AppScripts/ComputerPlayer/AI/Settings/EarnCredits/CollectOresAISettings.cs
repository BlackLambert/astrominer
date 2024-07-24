using UnityEngine;

namespace SBaier.Astrominer
{
    [CreateAssetMenu(fileName = "CollectOresAISettings", menuName = "ScriptableObjects/AIActions/CollectOresAISettings")]
    public class CollectOresAISettings : ScriptableObject
    {
        [SerializeField] private float _baseWeight = 0;
        public float BaseWeight => _baseWeight;
        
        [field: SerializeField]
        public float CollectValueFactor { get; private set; }
        
        [SerializeField] private AiCollectOresTargetSettings _collectOresTargetSettings;
        public AiCollectOresTargetSettings CollectOresTargetSettings => _collectOresTargetSettings;

        [SerializeField] private AnimationCurve _currentCreditsFactorCurve;
        public AnimationCurve CurrentCreditsFactorCurve => _currentCreditsFactorCurve;

        [SerializeField] private float _currentCreditsFactor;
        public float CurrentCreditsFactor => _currentCreditsFactor;
    }
}