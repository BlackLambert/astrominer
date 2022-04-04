using UnityEngine;

namespace SBaier.Astrominer
{
    [CreateAssetMenu(fileName = "MiningSettings", menuName = "ScriptableObjects/MiningSettings")]
    public class MiningSettings : ScriptableObject
    {
        [field: SerializeField]
        public float BaseSecondsTillExploit { get; private set; } = 120f;
        [field: SerializeField]
        public float QualityMineSpeedAddition { get; private set; } = 0.25f;
    }
}
