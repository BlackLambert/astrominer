using UnityEngine;

namespace SBaier.Astrominer
{
    [CreateAssetMenu(fileName = "DroneSettings", menuName = "ScriptableObjects/DroneSettings")]
    public class DroneSettings : ScriptableObject
    {
        [field: SerializeField]
        public float MaxSpeedPerSecond { get; private set; } = 10;
		
        [field: SerializeField]
        public float Acceleration { get; private set; } = 8;
		
        [field: SerializeField]
        public float BreakForce { get; private set; } = 6;

        [field: SerializeField] 
        public float Price { get; private set; } = 300;
    }
}
