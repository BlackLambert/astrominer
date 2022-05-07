using SBaier.DI;
using UnityEngine;
using UnityEngine.UI;

namespace SBaier.Astrominer
{
    public class ImageColorSetter : MonoBehaviour, Injectable
    {
        [SerializeField]
        private Image _image;

        private Color _color;

        public void Inject(Resolver resolver)
        {
            _color = resolver.Resolve<Color>();
        }

        private void OnEnable()
        {
            _image.color = _color;
        }
    }
}
