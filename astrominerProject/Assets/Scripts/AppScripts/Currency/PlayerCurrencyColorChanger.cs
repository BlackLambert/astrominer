using SBaier.DI;
using TMPro;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class PlayerCurrencyColorChanger : MonoBehaviour, Injectable, Initializable, Cleanable
    {
        [SerializeField] 
        private TextMeshProUGUI _text;

        [SerializeField] 
        private Color _negativeColor = Color.red;

        [SerializeField] 
        private Color _positiveColor = Color.white;

        private Currency _currency;

        public void Inject(Resolver resolver)
        {
            _currency = resolver.Resolve<Player>().Credits;
        }

        public void Initialize()
        {
            UpdateColor();
            _currency.OnAmountChanged += UpdateColor;
        }

        public void Clean()
        {
            _currency.OnAmountChanged -= UpdateColor;
        }

        private void UpdateColor()
        {
            _text.color = _currency.Amount < 0 ? _negativeColor : _positiveColor;
        }
    }
}
