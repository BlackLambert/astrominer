using UnityEngine;
using TMPro;
using SBaier.DI;

namespace SBaier.Astrominer
{
    public class AstroidsAmountOptionDisplay : MonoBehaviour, Injectable
    {
        [SerializeField]
        private TextMeshProUGUI _text;

        private AsteroidAmountOption _option;

        public void Inject(Resolver resolver)
        {
            _option = resolver.Resolve<AsteroidAmountOption>();
        }

        private void OnEnable()
        {
            _text.text = _option.Amount.ToString();
        }
    }
}
