using UnityEngine;
using TMPro;
using SBaier.DI;

namespace SBaier.Astrominer
{
    public class AsteroidsAmountOptionDisplay : MonoBehaviour, Injectable, Initializable
    {
        [SerializeField]
        private TextMeshProUGUI _text;

        private AsteroidAmountOption _option;

        public void Inject(Resolver resolver)
        {
            _option = resolver.Resolve<AsteroidAmountOption>();
        }

        public void Initialize()
        {
            _text.text = _option.Amount.ToString();
        }
    }
}
