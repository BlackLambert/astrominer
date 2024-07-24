using UnityEngine;

namespace SBaier.Astrominer
{
    [CreateAssetMenu(fileName = "AIOresToSellSettings", menuName = "ScriptableObjects/AIActions/AIOresToSellSettings")]
    public class AIOresToSellSettings : ScriptableObject
    {
        [field: SerializeField] public AnimationCurve PriceRangeFactorCurve { get; private set; }
        [field: SerializeField] public float PriceRangeFactor { get; private set; } = 5;
        
        [field: SerializeField] public AnimationCurve CurrentCreditsFactorCurve { get; private set; }
        [field: SerializeField] public float CurrentCreditsFactor { get; private set; } = 5;
    }
}