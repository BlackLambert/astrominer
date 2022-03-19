using UnityEngine;

namespace SBaier.Astrominer
{

    [CreateAssetMenu(fileName = "AsteroidSettings", menuName = "ScriptableObjects/AsteroidSettings")]
    public class AsteroidSettings : ScriptableObject
    {
        [field: SerializeField]
        public Vector2 ResourceAmountRange { get; private set; } = new Vector2(6, 10);
        public float MinResourceAmount => ResourceAmountRange.x;
        public float MaxResourceAmount => ResourceAmountRange.y;

        [field: SerializeField]
        public Vector2 SizeRange { get; private set; } = new Vector2(1, 2);
        public float MinSize => SizeRange.x;
        public float MaxSize => SizeRange.y;
        [field: SerializeField]
        public Vector2 MinPosition { get; private set; } = new Vector2(-15, -10);
        [field: SerializeField]
        public Vector2 MaxPosition { get; private set; } = new Vector2(15, 10);
    }
}
