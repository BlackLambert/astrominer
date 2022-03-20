using UnityEngine;

namespace SBaier.Astrominer
{
    [CreateAssetMenu(fileName = "VisualsSettings", menuName = "ScriptableObjects/VisualsSettings")]
    public class VisualsSettings : ScriptableObject
    {
        [field: SerializeField]
        public Color SelectIndicatorColor { get; private set; } = Color.white;
        [field: SerializeField]
        public Color ActionRadiusColor { get; private set; } = new Color(0, 0, 1, 0.2f);
    }
}
