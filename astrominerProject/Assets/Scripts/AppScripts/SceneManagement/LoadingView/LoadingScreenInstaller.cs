using SBaier.DI;

namespace SBaier.Astrominer
{
    public class LoadingScreenInstaller : MonoInstaller, Injectable
    {
        private SceneChangeProcess _process;
        
        public override void InstallBindings(Binder binder)
        {
            binder.BindInstance(_process);
        }

        public void Inject(Resolver resolver)
        {
            _process = resolver.Resolve<SceneChangeProcess>();
        }
    }
}