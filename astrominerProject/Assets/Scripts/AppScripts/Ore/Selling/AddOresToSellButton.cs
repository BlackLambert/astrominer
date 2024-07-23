using SBaier.DI;
using UnityEngine;
using UnityEngine.UI;

namespace SBaier.Astrominer
{
    public class AddOresToSellButton : MonoBehaviour, Injectable, Initializable, Cleanable
    {
        [SerializeField] private Button _button;

        [SerializeField] private int _delta;

        private int _oresToSellAmount => (int)_oresToSell.Ores[_oreType].Amount;
        private int _collectedOresAmount => (int)_ship.CollectedOres[_oreType].Amount;

        private OreType _oreType;
        private OresToSell _oresToSell;
        private Ship _ship;

        private void Reset()
        {
            _button = GetComponent<Button>();
        }

        public void Inject(Resolver resolver)
        {
            _oreType = resolver.Resolve<OreType>();
            _oresToSell = resolver.Resolve<OresToSell>();
            _ship = resolver.Resolve<Ship>();
        }

        public void Initialize()
        {
            _button.onClick.AddListener(OnClick);
            _oresToSell.Ores.OnValueChanged += UpdateInteractable;
            _ship.CollectedOres.OnValueChanged += UpdateInteractable;
            UpdateInteractable();
        }

        public void Clean()
        {
            _button.onClick.RemoveListener(OnClick);
            _oresToSell.Ores.OnValueChanged -= UpdateInteractable;
            _ship.CollectedOres.OnValueChanged -= UpdateInteractable;
        }

        private void OnClick()
        {
            _oresToSell.Ores.ChangeBy(_oreType, GetDelta());
            UpdateInteractable();
        }

        private void UpdateInteractable()
        {
            _button.interactable = GetDelta() != 0;
        }

        private int GetDelta()
        {
            int oresToSell = _oresToSellAmount;
            return Mathf.Clamp(_delta, -oresToSell, _collectedOresAmount - oresToSell);
        }
    }
}