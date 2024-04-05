using SBaier.DI;

namespace SBaier.Astrominer
{
    public class GameEndScreenInstaller : MonoInstaller, Injectable
    {
        private Game _game;

        public void Inject(Resolver resolver)
        {
            _game = resolver.Resolve<Game>();
        }
        
        public override void InstallBindings(Binder binder)
        {
            binder.BindInstance(_game.PLayerWon.Value);
            binder.BindInstance(_game.PLayerWon.Value.Color);
        }
    }
}
