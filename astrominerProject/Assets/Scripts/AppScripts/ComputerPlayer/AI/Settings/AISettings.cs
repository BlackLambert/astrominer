using UnityEngine;

namespace SBaier.Astrominer
{
    [CreateAssetMenu(fileName = "AISettings", menuName = "ScriptableObjects/AISettings")]
    public class AISettings : ScriptableObject
    {
        [field: SerializeField]
        public bool EnableLogging { get; private set; }
    }
}