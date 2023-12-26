using UnityEngine;

namespace SBaier.Astrominer
{
    [CreateAssetMenu(fileName = "ProspectorDroneSettings", menuName = "ScriptableObjects/ProspectorDroneSettings")]
    public class ProspectorDroneSettings : ScriptableObject
    {
        [field: SerializeField]
        public float MaxSpeedPerSecond { get; private set; } = 10;
		
        [field: SerializeField]
        public float Acceleration { get; private set; } = 8;
		
        [field: SerializeField]
        public float BreakForce { get; private set; } = 6;
    }
}
