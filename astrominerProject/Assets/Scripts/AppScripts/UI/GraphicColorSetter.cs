using SBaier.DI;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace SBaier.Astrominer
{
    public class GraphicColorSetter : MonoBehaviour, Injectable
    {
        [FormerlySerializedAs("_image")] [SerializeField]
        private Graphic _graphic;

        private Color _color;

        public void Inject(Resolver resolver)
        {
            _color = resolver.Resolve<Color>();
        }

        private void OnEnable()
        {
            _graphic.color = _color;
        }
    }
}
