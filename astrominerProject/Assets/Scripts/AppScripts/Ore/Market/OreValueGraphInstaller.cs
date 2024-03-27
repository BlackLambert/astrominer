using SBaier.DI;

namespace SBaier.Astrominer
{
    public class OreValueGraphInstaller : MonoInstaller, Injectable
    {
        private OresSettings.OreSettings _oreSettings;

        public void Inject(Resolver resolver)
        {
            _oreSettings = resolver.Resolve<OresSettings.OreSettings>();
        }
        
        public override void InstallBindings(Binder binder)
        {
            binder.BindInstance(_oreSettings.Type);
            binder.BindInstance(_oreSettings.Color);
        }
    }
}
