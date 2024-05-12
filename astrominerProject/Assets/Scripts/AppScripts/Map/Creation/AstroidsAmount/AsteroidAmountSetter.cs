using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class AsteroidAmountSetter : MonoBehaviour, Injectable, Initializable, Cleanable
    {
        private Observable<int> _asteroidsAmount;
        private ActiveItem<AsteroidAmountOption> _selectedAsteroidsAmountOption;
        private MapCreationSettings _settings;
        private Players _players;

        public void Inject(Resolver resolver)
        {
            _asteroidsAmount = resolver.Resolve<Observable<int>>();
            _selectedAsteroidsAmountOption = resolver.Resolve<ActiveItem<AsteroidAmountOption>>();
            _settings = resolver.Resolve<MapCreationSettings>();
            _players = resolver.Resolve<Players>();
        }

        public void Initialize()
        {
            UpdateAmountOption(_asteroidsAmount.Value);
            _asteroidsAmount.OnValueChanged += OnAsteroidsAmountChanged;
        }

        public void Clean()
        {
            _asteroidsAmount.OnValueChanged += OnAsteroidsAmountChanged;
        }

        private void OnAsteroidsAmountChanged(int formervalue, int newvalue)
        {
            UpdateAmountOption(newvalue);
        }

        private void UpdateAmountOption(int amount)
        {
            int minAmount = _players.Count * _settings.MinAsteroidsAdditionPerPlayer + _settings.MinAsteroids;
            int amountRange = _settings.MaxAsteroidsAmount - minAmount;
            float percentage = (float)(amount - minAmount) / amountRange;

            float cameraSizeRange = _settings.CameraSizeRange.y - _settings.CameraSizeRange.x;
            float cameraSize = _settings.CameraSizeRange.x + cameraSizeRange * percentage;
            
            Vector2 mapSizeRange = _settings.EndMapSize - _settings.StartMapSize;
            Vector2 size = _settings.StartMapSize + mapSizeRange * percentage;

            AsteroidAmountOption amountOption = new AsteroidAmountOption(
                amount, size, cameraSize, Vector2.zero);
            _selectedAsteroidsAmountOption.Value = amountOption;
        }
    }
}
