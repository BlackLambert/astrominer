using UnityEngine;

namespace SBaier.Astrominer
{
    [CreateAssetMenu(fileName = "FlyToUnidentifiedAsteroidAISettings",
        menuName = "ScriptableObjects/AIActions/FlyToUnidentifiedAsteroidAISettings")]
    public class FlyToUnidentifiedAsteroidAISettings : ScriptableObject
    {
        [SerializeField] private float _baseWeight = 0;
        public float BaseWeight => _baseWeight;

        [field: SerializeField] 
        public AIProspectingSettings ProspectingSettings { get; private set; }
        
        [field: SerializeField]
        public float ProspectingValueFactor { get; private set; }
    }
}