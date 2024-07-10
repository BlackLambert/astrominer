using UnityEngine;

namespace SBaier.Astrominer
{
    [CreateAssetMenu(fileName = "SendCarrierDroneAISettings", menuName = "ScriptableObjects/AIActions/SendCarrierDroneAISettings")]
    public class SendCarrierDroneAISettings : ScriptableObject
    {
        [SerializeField] private float _baseWeight = 0;
        public float BaseWeight => _baseWeight;

        [SerializeField] private AnimationCurve _currentCreditsFactorCurve;
        public AnimationCurve CurrentCreditsFactorCurve => _currentCreditsFactorCurve;

        [SerializeField] private float _currentCreditsFactor;
        public float CurrentCreditsFactor => _currentCreditsFactor;
        
        [SerializeField] private AiCollectOresTargetSettings _collectOresTargetSettings;
        public AiCollectOresTargetSettings CollectOresTargetSettings => _collectOresTargetSettings;
    }
}