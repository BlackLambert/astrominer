using UnityEngine;

namespace SBaier.Astrominer
{
    [CreateAssetMenu(fileName = "SellMachineAISettings", menuName = "ScriptableObjects/AIActions/SellMachineAISettings")]
    public class SellMachineAISettings : ScriptableObject
    {
        [field: SerializeField] 
        public float BaseWeight { get; private set; } = 0;

        [field: SerializeField] 
        public float NoOccupationTargetWeight { get; private set; } = 5;
        
        [field: SerializeField] 
        public AnimationCurve BestOccupationTargetCurve { get; private set; } = new AnimationCurve();

        [field: SerializeField] 
        public float BestOccupationTargetFactor { get; private set; } = 5;
        
        [SerializeField] 
        private AnimationCurve _exploiterAmountValueCurve;
        public AnimationCurve ExploiterAmountValueCurve => _exploiterAmountValueCurve;

        [SerializeField] private float 
            _exploiterAmountFactor;
        public float ExploiterAmountFactor => _exploiterAmountFactor;
    }
}