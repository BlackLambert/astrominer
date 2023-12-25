using SBaier.DI;

namespace SBaier.Astrominer
{
    public class ShipsInstaller : MonoInstaller
    {
        public override void InstallBindings(Binder binder)
        {
            binder.Bind<ActiveItem<Ship>>().And<ActiveShip>().ToNew<ActiveShip>().AsSingle();
            binder.BindToNewSelf<QueuedShips>().AsSingle();
            binder.BindToNewSelf<Ships>().AsSingle();
        }
    }
}
