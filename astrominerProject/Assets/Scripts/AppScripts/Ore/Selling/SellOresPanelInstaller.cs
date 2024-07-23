using SBaier.DI;

namespace SBaier.Astrominer
{
    public class SellOresPanelInstaller : MonoInstaller, Injectable
    {
        private ActiveItem<Ship> _activeShip;

        public void Inject(Resolver resolver)
        {
            _activeShip = resolver.Resolve<ActiveItem<Ship>>();
        }
        
        public override void InstallBindings(Binder binder)
        {
            binder.BindToNewSelf<OresToSell>().AsSingle();
            binder.BindInstance(_activeShip.Value);
            binder.BindInstance(_activeShip.Value.Player);
        }
    }
}
