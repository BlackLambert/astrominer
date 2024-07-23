using UnityEngine;

namespace SBaier.Astrominer
{
    [CreateAssetMenu(fileName = "IdentifyAsteroidAISettings", menuName = "ScriptableObjects/AIActions/IdentifyAsteroidAISettings")]
    public class IdentifyAsteroidAISettings : ScriptableObject
    {
        [SerializeField] private float _baseWeight = 10;
        public float BaseWeight => _baseWeight;

        [SerializeField] private float _activeDronesFactor = -3;
        public float ActiveDronesFactor => _activeDronesFactor;

        [SerializeField] private float _prospectingTargetValueFactor;
        public float ProspectingTargetValueFactor => _prospectingTargetValueFactor;

        [SerializeField] private float _identifiedEmptyAsteroidsValueFactor;
        public float IdentifiedEmptyAsteroidsValueFactor => _identifiedEmptyAsteroidsValueFactor;

        [SerializeField] private AnimationCurve _identifiedEmptyAsteroidsValueFactorCurve;
        public AnimationCurve IdentifiedEmptyAsteroidsValueFactorCurve => _identifiedEmptyAsteroidsValueFactorCurve;

        [SerializeField] private float _bestIdentifiedEmptyAsteroidValueFactor;
        public float BestIdentifiedEmptyAsteroidValueFactor => _bestIdentifiedEmptyAsteroidValueFactor;

        [SerializeField] private AnimationCurve _bestIdentifiedEmptyAsteroidValueFactorCurve;
        public AnimationCurve BestIdentifiedEmptyAsteroidValueFactorCurve => _bestIdentifiedEmptyAsteroidValueFactorCurve;
    }
}