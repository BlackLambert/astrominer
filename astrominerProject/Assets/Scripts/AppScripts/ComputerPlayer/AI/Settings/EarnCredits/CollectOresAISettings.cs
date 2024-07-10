using UnityEngine;

namespace SBaier.Astrominer
{
    [CreateAssetMenu(fileName = "CollectOresAISettings", menuName = "ScriptableObjects/AIActions/CollectOresAISettings")]
    public class CollectOresAISettings : ScriptableObject
    {
        [SerializeField] private float _baseWeight = 0;
        public float BaseWeight => _baseWeight;
        
        [SerializeField] private AiCollectOresTargetSettings _collectOresTargetSettings;
        public AiCollectOresTargetSettings CollectOresTargetSettings => _collectOresTargetSettings;
    }
}