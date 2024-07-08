using UnityEngine;

namespace SBaier.Astrominer
{
    [CreateAssetMenu(fileName = "IdentifyAsteroidAISettings", menuName = "ScriptableObjects/AIActions/IdentifyAsteroidAISettings")]
    public class IdentifyAsteroidAISettings : ScriptableObject
    {
        [SerializeField] private float _baseWeight = 10;
        public float BaseWeight => _baseWeight;

        [SerializeField] private float _identifiedEmptyAsteroidsValueFactor;
        public float IdentifiedEmptyAsteroidsValueFactor => _identifiedEmptyAsteroidsValueFactor;

        [SerializeField] private float _activeDronesFactor = -3;
        public float ActiveDronesFactor => _activeDronesFactor;

        [SerializeField] private AnimationCurve _identifiedEmptyAsteroidsValueFactorCurve;
        public AnimationCurve IdentifiedEmptyAsteroidsValueFactorCurve => _identifiedEmptyAsteroidsValueFactorCurve;
    }
}