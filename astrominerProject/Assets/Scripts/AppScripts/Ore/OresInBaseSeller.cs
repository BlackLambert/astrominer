using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class OresInBaseSeller : MonoBehaviour, Injectable
    {
        private OreBank _bank;
        private Ores _ores;
        private Player _player;
        private Flyable _flyable;
        
        public void Inject(Resolver resolver)
        {
            _bank = resolver.Resolve<OreBank>();
            _ores = resolver.Resolve<Ores>();
            _player = resolver.Resolve<Player>();
            _flyable = resolver.Resolve<Flyable>();
        }

        private void OnEnable()
        {
            _flyable.Location.OnValueChanged += OnLocationChanged;
        }

        private void OnDisable()
        {
            _flyable.Location.OnValueChanged -= OnLocationChanged;
        }

        private void OnLocationChanged(FlyTarget formervalue, FlyTarget newvalue)
        {
            if (newvalue is Base playerBase && playerBase.Player == _player)
            {
                _player.Credits.Add(_bank.CalculateCreditsFor(_ores.RequestAll()));
            }
        }
    }
}
