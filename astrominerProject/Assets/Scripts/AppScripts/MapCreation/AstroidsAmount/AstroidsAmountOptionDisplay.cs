using UnityEngine;
using TMPro;
using SBaier.DI;

namespace SBaier.Astrominer
{
    public class AstroidsAmountOptionDisplay : MonoBehaviour, Injectable
    {
        [SerializeField]
        private TextMeshProUGUI _text;

        private AstroidAmountOption _option;

        public void Inject(Resolver resolver)
        {
            _option = resolver.Resolve<AstroidAmountOption>();
        }

        private void OnEnable()
        {
            _text.text = _option.Amount.ToString();
        }
    }
}
