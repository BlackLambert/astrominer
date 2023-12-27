using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class AsteroidAmountSetter : MonoBehaviour, Injectable
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

        private void OnEnable()
        {
            UpdateAmountOption();
            _asteroidsAmount.OnValueChanged += OnAsteroidsAmountChanged;
        }

        private void OnDisable()
        {
            _asteroidsAmount.OnValueChanged += OnAsteroidsAmountChanged;
        }

        private void OnAsteroidsAmountChanged(int formervalue, int newvalue)
        {
            UpdateAmountOption();
        }

        private void UpdateAmountOption()
        {
            int minAmount = _players.Count * _settings.MinAsteroidsAdditionPerPlayer + _settings.MinAsteroids;
            int amountRange = _settings.MaxAsteroidsAmount - minAmount;
            float percentage = (float)(_asteroidsAmount - minAmount) / amountRange;

            float cameraSizeRange = _settings.CameraSizeRange.y - _settings.CameraSizeRange.x;
            float cameraSize = _settings.CameraSizeRange.x + cameraSizeRange * percentage;
            
            Vector2 mapSizeRange = _settings.EndMapSize - _settings.StartMapSize;
            Vector2 size = _settings.StartMapSize + mapSizeRange * percentage;

            AsteroidAmountOption amountOption = new AsteroidAmountOption(
                _asteroidsAmount, size, cameraSize, Vector2.zero);
            _selectedAsteroidsAmountOption.Value = amountOption;
        }
    }
}
