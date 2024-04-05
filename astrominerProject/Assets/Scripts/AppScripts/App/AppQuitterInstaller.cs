using SBaier.DI;

namespace SBaier.Astrominer
{
    public class AppQuitterInstaller : MonoInstaller
    {
        public override void InstallBindings(Binder binder)
        {
#if UNITY_EDITOR
            binder.Bind<AppQuitter>().ToNew<EditorAppQuitter>().AsSingle();
#else
            binder.Bind<AppQuitter>().ToNew<StandaloneAppQuitter>().AsSingle();
#endif
        }
    }
}
