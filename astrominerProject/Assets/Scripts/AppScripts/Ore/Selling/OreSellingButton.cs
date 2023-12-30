using SBaier.DI;
using UnityEngine;
using UnityEngine.UI;

namespace SBaier.Astrominer
{
    public class OreSellingButton : MonoBehaviour, Injectable
    {
        [SerializeField]
        private Button _button;

        private OreBank _bank;
        private Ship _ship;
        private Player _player;

        public void Inject(Resolver resolver)
        {
            _bank = resolver.Resolve<OreBank>();
            _ship = resolver.Resolve<Ship>();
            _player = resolver.Resolve<Player>();
        }

        private void OnEnable()
        {
            UpdateInteractable();
            _button.onClick.AddListener(OnClick);
            _ship.Location.OnValueChanged += OnLocationChanged;
            _ship.CollectedOres.OnValueChanged += UpdateInteractable;
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnClick);
            _ship.Location.OnValueChanged -= OnLocationChanged;
            _ship.CollectedOres.OnValueChanged -= UpdateInteractable;
        }

        private void OnLocationChanged(FlyTarget formervalue, FlyTarget newvalue)
        {
            UpdateInteractable();
        }

        private void OnClick()
        {
            float credits = _bank.CalculateCreditsFor(_ship.CollectedOres.RequestAll());
            _player.Credits.Add(credits);
        }

        private void UpdateInteractable()
        {
            bool hasCollectedOres = !_ship.CollectedOres.IsEmpty();
            bool isAtBase = _ship.Location.Value is Base;
            _button.interactable = hasCollectedOres && isAtBase;
        }
    }
}
