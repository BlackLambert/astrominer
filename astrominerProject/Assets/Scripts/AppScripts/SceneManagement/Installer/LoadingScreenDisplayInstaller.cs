using SBaier.DI;

namespace SBaier.Astrominer
{
    public class LoadingScreenDisplayInstaller : MonoInstaller
    {
        public override void InstallBindings(Binder binder)
        {
            binder.BindComponent<LoadingScreenDisplayer>()
                .FromNewComponentOnNewGameObject("LoadingScreenDisplayer", transform).AsNonResolvable();
        }
    }
}
