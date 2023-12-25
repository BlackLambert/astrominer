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
        public string AsteroidName { get; private set; } = "Asteroid {0}";
        [field: SerializeField]
        public float MaxObjectSizeAddition { get; private set; } = 0.3f;
        [field: SerializeField]
        public float StartObjectSize { get; private set; } = 0.7f;
        [field: SerializeField]
        public Color Color { get; private set; } = Color.white;
        [field: SerializeField]
        public Color ExploitedColorReduction { get; private set; } = new Color(0.2f, 0.2f, 0.2f, 0);

        
        [field: SerializeField, Header("Ores")]
        public float BaseRockAmount { get; private set; } = 500;
        [field: SerializeField]
        public float IronWeight { get; private set; } = 65;
        [field: SerializeField]
        public float GoldWeight { get; private set; } = 25;
        [field: SerializeField]
        public float PlatinumWeight { get; private set; } = 10;
        public float OreWeightSum => IronWeight + GoldWeight + PlatinumWeight;
    }
}
