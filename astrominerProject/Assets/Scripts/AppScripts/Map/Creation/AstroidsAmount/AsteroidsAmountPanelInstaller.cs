using SBaier.DI;

namespace SBaier.Astrominer
{
    public class AsteroidsAmountPanelInstaller : MonoInstaller
    {
        public override void InstallBindings(Binder binder)
        {
            binder.BindToNewSelf<Selection>().AsSingle();
        }
    }
}
