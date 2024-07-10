using UnityEngine;

namespace SBaier.Astrominer
{
    [CreateAssetMenu(fileName = "SellOresAISettings", menuName = "ScriptableObjects/AIActions/SellOresAISettings")]
    public class SellOresAISettings : ScriptableObject
    {
        [SerializeField] private float _baseWeight = 0;
        public float BaseWeight => _baseWeight;

        [SerializeField] private AnimationCurve _storedShipOresValueFactorCurve;
        public AnimationCurve StoredShipOresValueFactorCurve => _storedShipOresValueFactorCurve;

        [SerializeField] private float _storedShipOresValueFactor;
        public float StoredShipOresValueFactor => _storedShipOresValueFactor;

        [SerializeField] private AnimationCurve _distanceToBaseFactorCurve;
        public AnimationCurve DistanceToBaseFactorCurve => _distanceToBaseFactorCurve;

        [SerializeField] private float _distanceToBaseFactor;
        public float DistanceToBaseFactor => _distanceToBaseFactor;
    }
}