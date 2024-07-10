using UnityEngine;

namespace SBaier.Astrominer
{
    [CreateAssetMenu(fileName = "OccupyAsteroidAiSettings", menuName = "ScriptableObjects/AIActions/OccupyAsteroidAiSettings")]
    public class OccupyAsteroidAiSettings : ScriptableObject
    {
        [SerializeField] private float _baseWeight = 10;
        public float BaseWeight => _baseWeight;

        [SerializeField] private AIExploitTargetSettings _exploitTargetSettings;
        public AIExploitTargetSettings ExploitTargetSettings => _exploitTargetSettings;

        [SerializeField] private float _occupationValueFactor = 1;
        public float OccupationValueFactor => _occupationValueFactor;
        
        [SerializeField] private AnimationCurve _occupationTargetsAmountValueCurve;
        public AnimationCurve OccupationTargetsAmountValueCurve => _occupationTargetsAmountValueCurve;

        [SerializeField] private float _occupationTargetsAmountFactor;
        public float OccupationTargetsAmountFactor => _occupationTargetsAmountFactor;
        
        [SerializeField] private AnimationCurve _exploiterAmountValueCurve;
        public AnimationCurve ExploiterAmountValueCurve => _exploiterAmountValueCurve;

        [SerializeField] private float _exploiterAmountFactor;
        public float ExploiterAmountFactor => _exploiterAmountFactor;
    }
}