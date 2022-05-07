using System.Collections.Generic;
using UnityEngine;

namespace SBaier.Astrominer
{
    [CreateAssetMenu(fileName = "PlayerSettings", menuName = "ScriptableObjects/PlayerSettings")]
    public class PlayerSettings : ScriptableObject
    {
        [field: SerializeField]
        public List<Color> PlayerColors { get; private set; } = new List<Color>();
        [field: SerializeField]
        public float StartCredits { get; private set; } = 10_000;
    }
}
