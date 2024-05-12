using SBaier.DI;
using UnityEngine;
using UnityEngine.UI;

namespace SBaier.Astrominer
{
    public class GraphicColorSetter : MonoBehaviour, Injectable, Initializable
    {
        [SerializeField]
        private Graphic _graphic;

        private Color _color;

        public void Inject(Resolver resolver)
        {
            _color = resolver.Resolve<Color>();
        }

        public void Initialize()
        {
            _graphic.color = _color;
        }

        private void Reset()
        {
            _graphic = GetComponent<Graphic>();
        }
    }
}
