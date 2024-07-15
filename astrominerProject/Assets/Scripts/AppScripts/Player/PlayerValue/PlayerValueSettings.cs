using UnityEngine;

namespace SBaier.Astrominer
{
    [CreateAssetMenu(fileName = "PlayerValueSettings", menuName = "ScriptableObjects/PlayerValueSettings")]
    public class PlayerValueSettings : ScriptableObject
    {
        [SerializeField] 
        private int _valueHistoryBufferSize = 30;

        public int ValueHistoryBufferSize => _valueHistoryBufferSize;

        [SerializeField] 
        private float updateFrequency = 1;

        public float UpdateFrequency => updateFrequency;

        [SerializeField] 
        private MinMax _valueOffset = new MinMax() { Min = -200, Max = 200 };

        public MinMax ValueOffset => _valueOffset;
    }
}
