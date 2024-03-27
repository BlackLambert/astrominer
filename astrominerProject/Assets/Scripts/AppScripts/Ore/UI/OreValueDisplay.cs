using SBaier.DI;
using TMPro;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class OreValueDisplay : MonoBehaviour, Injectable
    {
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private string _format = "F1";
        
        private OreType _oreType;
        private OreValue _oreValue;
        
        public void Inject(Resolver resolver)
        {
            _oreType = resolver.Resolve<OreType>();
            _oreValue = resolver.Resolve<OreValue>();
        }

        private void OnEnable()
        {
            UpdateText(_oreValue.GetValue(_oreType));
            _oreValue.OnValueChanged += OnOreValueChanged;
        }

        private void OnDisable()
        {
            _oreValue.OnValueChanged -= OnOreValueChanged;
        }

        private void UpdateText(float value)
        {
            _text.text = value.ToString(_format);
        }

        private void OnOreValueChanged(OreType oreType, float value)
        {
            if (oreType != _oreType)
            {
                return;
            }

            UpdateText(value);
        }
    }
}
