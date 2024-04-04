using SBaier.DI;

namespace SBaier.Astrominer
{
    public class PlayerValueCalculatorInstaller : MonoInstaller, Injectable
    {
        private Player _player;
        private PlayerValues _playerValues;

        public void Inject(Resolver resolver)
        {
            _player = resolver.Resolve<Player>();
            _playerValues = resolver.Resolve<PlayerValues>();
        }
        
        public override void InstallBindings(Binder binder)
        {
            binder.BindInstance(_player).WithoutInjection();
            binder.BindInstance(_playerValues.Values[_player]).WithoutInjection();
        }
    }
}
