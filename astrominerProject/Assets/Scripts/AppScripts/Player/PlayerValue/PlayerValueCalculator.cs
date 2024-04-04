using System;
using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class PlayerValueCalculator : MonoBehaviour, Injectable
    {
        [SerializeField]
        private float _updateFrequence = 1;
        
        private Player _player;
        private GameTime _gameTime;
        private OreBank _bank;
        private ExploitMachineVendor _exploitMachineVendor;

        private float _timeTillNextEvaluation = 0;
        
        public void Inject(Resolver resolver)
        {
            _player = resolver.Resolve<Player>();
            _gameTime = resolver.Resolve<GameTime>();
            _bank = resolver.Resolve<OreBank>();
            _exploitMachineVendor = resolver.Resolve<ExploitMachineVendor>();
        }

        private void OnEnable()
        {
            _player.ResetPlayerValue();
        }

        private void Update()
        {
            while (_timeTillNextEvaluation <= _gameTime.Value)
            {
                _timeTillNextEvaluation += _updateFrequence;
                float value = CalculateValue();
                _player.AddPlayerValue(value);
                Debug.Log($"Player {_player.Name} Value {value}");
            }
        }

        private float CalculateValue()
        {
            float value = 0;

            value += CalculatePlayerMoney();
            value += CalculatePlayerShipMoney();
            
            return value;
        }

        private float CalculatePlayerMoney()
        {
            float value = 0;
            value += _player.Credits.Amount;
            
            foreach (Asteroid asteroid in _player.OwnedAsteroids)
            {
                value += _bank.CalculateCreditsFor(asteroid.MinedOres);
                value += _exploitMachineVendor.CalculateSellValue(asteroid.ExploitMachine);
            }
            
            return value;
        }

        private float CalculatePlayerShipMoney()
        {
            if (_player.Ship.Value == null)
            {
                return 0;
            }
            
            float value = 0;
            value += _bank.CalculateCreditsFor(_player.Ship.Value.CollectedOres);

            foreach (ExploitMachine machine in _player.Ship.Value.Machines)
            {
                value += _exploitMachineVendor.CalculateSellValue(machine);
            }

            return value;
        }
    }
}
