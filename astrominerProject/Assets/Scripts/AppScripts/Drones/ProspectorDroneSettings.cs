using UnityEngine;

namespace SBaier.Astrominer
{
    [CreateAssetMenu(fileName = "ProspectorDroneSettings", menuName = "ScriptableObjects/ProspectorDroneSettings")]
    public class ProspectorDroneSettings : ScriptableObject
    {
        [field: SerializeField]
        public float SpeedPerSecond { get; private set; } = 2;
    }
}
