using SBaier.DI;
using UnityEngine;
using UnityEngine.UI;

namespace SBaier.Astrominer
{
    public class SellOresButton : MonoBehaviour, Injectable, Initializable, Cleanable
    {
        [SerializeField] 
        private Button _button;

        private OresToSell _oresToSell;
        private OreBank _oreBank;
        private Ship _ship;
        private Player _player;

        public void Inject(Resolver resolver)
        {
            _oresToSell = resolver.Resolve<OresToSell>();
            _oreBank = resolver.Resolve<OreBank>();
            _ship = resolver.Resolve<Ship>();
            _player = resolver.Resolve<Player>();
        }

        public void Initialize()
        {
            _oresToSell.Ores.OnValueChanged += UpdateInteractability;
            _button.onClick.AddListener(OnClick);
            UpdateInteractability();
        }

        public void Clean()
        {
            _oresToSell.Ores.OnValueChanged -= UpdateInteractability;
            _button.onClick.RemoveListener(OnClick);
        }

        private void UpdateInteractability()
        {
            _button.interactable = _oreBank.CalculateCreditsFor(_oresToSell.Ores) > 0;
        }

        private void OnClick()
        {
            Ores deltaOres = _oresToSell.Ores.RequestAll();
            _player.Credits.Add(_oreBank.CalculateCreditsFor(deltaOres));
            _ship.CollectedOres.Request(deltaOres);
            UpdateInteractability();
        }
    }
}
