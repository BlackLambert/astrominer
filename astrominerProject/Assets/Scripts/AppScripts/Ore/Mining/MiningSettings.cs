using UnityEngine;

namespace SBaier.Astrominer
{
    [CreateAssetMenu(fileName = "MiningSettings", menuName = "ScriptableObjects/MiningSettings")]
    public class MiningSettings : ScriptableObject
    {
        [field: SerializeField]
        public float BaseAsteroidBodyMaterialPerSecond { get; private set; } = 25f;
    }
}
