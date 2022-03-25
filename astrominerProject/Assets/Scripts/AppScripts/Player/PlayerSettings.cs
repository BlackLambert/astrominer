using UnityEngine;

namespace SBaier.Astrominer
{
    [CreateAssetMenu(fileName = "PlayerSettings", menuName = "ScriptableObjects/PlayerSettings")]
    public class PlayerSettings : ScriptableObject
    {
        [field: SerializeField]
        public Color PlayerColor { get; private set; } = Color.red;
        [field: SerializeField]
        public float StartCredits { get; private set; } = 10_000;
    }
}
