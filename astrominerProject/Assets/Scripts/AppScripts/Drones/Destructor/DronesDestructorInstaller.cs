using SBaier.DI;

namespace SBaier.Astrominer
{
    public class DronesDestructorInstaller : MonoInstaller, Injectable
    {
        private Player _player;

        public void Inject(Resolver resolver)
        {
            _player = resolver.Resolve<Player>();
        }
        
        public override void InstallBindings(Binder binder)
        {
            binder.BindInstance(_player.Drones)
                .WithoutInjection();
        }
    }
}
