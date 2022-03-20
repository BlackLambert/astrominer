using UnityEngine;

namespace SBaier.Astrominer
{

    [CreateAssetMenu(fileName = "AsteroidSettings", menuName = "ScriptableObjects/AsteroidSettings")]
    public class AsteroidSettings : ScriptableObject
    {
        [field: SerializeField]
        public Vector2Int QualityAmountRange { get; private set; } = new Vector2Int(3, 10);
        public int MinQuality => QualityAmountRange.x;
        public int MaxQuality => QualityAmountRange.y;

        [field: SerializeField]
        public Vector2Int SizeRange { get; private set; } = new Vector2Int(3, 10);
        public int MinSize => SizeRange.x;
        public int MaxSize => SizeRange.y;
        [field: SerializeField]
        public Vector2 MinPosition { get; private set; } = new Vector2(-15, -10);
        [field: SerializeField]
        public Vector2 MaxPosition { get; private set; } = new Vector2(15, 10);
    }
}
