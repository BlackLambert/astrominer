using System;
using SBaier.DI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SBaier.Astrominer
{
    public class OresToSellInput : MonoBehaviour, Injectable, Initializable, Cleanable
    {
        [SerializeField] private TMP_InputField _inputField;

        private OresToSell _oresToSell;
        private Ship _ship;
        private OreType _oreType;


        public void Inject(Resolver resolver)
        {
            _oresToSell = resolver.Resolve<OresToSell>();
            _oreType = resolver.Resolve<OreType>();
            _ship = resolver.Resolve<Ship>();
        }

        private void Reset()
        {
            _inputField = GetComponent<TMP_InputField>();
        }

        public void Initialize()
        {
            UpdateLabel();
            _oresToSell.Ores.OnValueChanged += OnOresChanged;
            _inputField.onEndEdit.AddListener(OnSubmit);
        }

        public void Clean()
        {
            _oresToSell.Ores.OnValueChanged -= OnOresChanged;
            _inputField.onEndEdit.RemoveListener(OnSubmit);
        }

        private void OnOresChanged()
        {
            UpdateLabel();
        }

        private void OnSubmit(string value)
        {
            if (!int.TryParse(value, out int amount))
            {
                throw new ArgumentException("The provided input text needs to be an integer number");
            }

            float floatAmount = Mathf.Clamp(amount, 0, _ship.CollectedOres[_oreType].Amount);
            _oresToSell.Ores.ChangeTo(_oreType, floatAmount);
        }

        private void UpdateLabel()
        {
            _inputField.SetTextWithoutNotify(_oresToSell.Ores[_oreType].Amount.ToString("F0"));
        }
    }
}
