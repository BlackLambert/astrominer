using UnityEngine;

namespace SBaier.Astrominer
{

    [CreateAssetMenu(fileName = "AsteroidSettings", menuName = "ScriptableObjects/AsteroidSettings")]
    public class AsteroidSettings : ScriptableObject
    {
        [field: SerializeField]
        public float MinResourceAmount { get; private set; } = 6;
        [field: SerializeField]
        public float MaxResourceAmount { get; private set; } = 10;
        [field: SerializeField]
        public Vector2 MinPosition { get; private set; } = new Vector2(-15, -10);
        [field: SerializeField]
        public Vector2 MaxPosition { get; private set; } = new Vector2(15, 10);
    }
}
