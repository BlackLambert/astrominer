using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class AsteroidAmountSliderInstaller : MonoInstaller, Injectable
    {
        private MapCreationSettings _settings;
        private Players _players;

        public void Inject(Resolver resolver)
        {
            _settings = resolver.Resolve<MapCreationSettings>();
            _players = resolver.Resolve<Players>();
        }

        public override void InstallBindings(Binder binder)
        {
            binder.BindInstance(new Observable<int>() { Value = _settings.StartAsteroidAmount });
            binder.BindInstance(new IntSliderInput.Arguments() { Range = CreateAmountRange() });
        }

        private Vector2Int CreateAmountRange()
        {
            return new Vector2Int(
                _players.Count * _settings.MinAsteroidsAdditionPerPlayer + _settings.MinAsteroids,
                _settings.MaxAsteroidsAmount);
        }
    }
}
