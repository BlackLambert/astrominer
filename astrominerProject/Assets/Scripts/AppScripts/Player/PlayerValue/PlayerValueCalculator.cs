using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class PlayerValueCalculator : MonoBehaviour, Injectable
    {
        [SerializeField]
        private float _updateFrequence = 1;
        
        private PlayerValue _playerValue;
        private Player _player;
        private GameTime _gameTime;
        private OreBank _bank;
        private ExploitMachineVendor _exploitMachineVendor;

        private float _timeTillNextEvaluation = 0;
        
        public void Inject(Resolver resolver)
        {
            _player = resolver.Resolve<Player>();
            _playerValue = resolver.Resolve<PlayerValue>();
            _gameTime = resolver.Resolve<GameTime>();
            _bank = resolver.Resolve<OreBank>();
            _exploitMachineVendor = resolver.Resolve<ExploitMachineVendor>();
        }

        private void Update()
        {
            while (_timeTillNextEvaluation <= _gameTime.Value)
            {
                _timeTillNextEvaluation += _updateFrequence;
                float value = CalculateValue();
                _playerValue.TotalValue.Value = value;
                _playerValue.ValueHistory.Add(value);
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
                value += _bank.CalculateCreditsFor(asteroid.StoredMinedOres);
                value += _exploitMachineVendor.CalculateSellValue(asteroid.ExploitMachine);
            }

            foreach (Drone drone in _player.Drones)
            {
                if (drone is CarrierDrone carrier)
                {
                    value += _bank.CalculateCreditsFor(carrier.CollectedOres);
                }
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
