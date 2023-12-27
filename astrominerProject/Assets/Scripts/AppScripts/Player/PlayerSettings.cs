using System.Collections.Generic;
using UnityEngine;

namespace SBaier.Astrominer
{
    [CreateAssetMenu(fileName = "PlayerSettings", menuName = "ScriptableObjects/PlayerSettings")]
    public class PlayerSettings : ScriptableObject
    {
        [field: SerializeField]
        public List<PlayerColorOption> PlayerColors { get; private set; } = new List<PlayerColorOption>();
        [field: SerializeField]
        public float StartCredits { get; private set; } = 10_000;
    }
}
