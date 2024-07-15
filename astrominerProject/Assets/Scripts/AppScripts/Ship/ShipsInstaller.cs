using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class ShipsInstaller : MonoInstaller
    {
        [SerializeField] 
        private ShipSettings _shipSettings;

        [SerializeField] 
        private ShipSettings _testShipSettings;

        [SerializeField] 
        private bool _useTestSettings;
        
        public override void InstallBindings(Binder binder)
        {
            binder.Bind<ActiveItem<Ship>>().And<ActiveShip>().ToNew<ActiveShip>().AsSingle();
            binder.BindToNewSelf<QueuedShips>().AsSingle();
            binder.BindToNewSelf<Ships>().AsSingle();
            binder.BindInstance(_useTestSettings ? _testShipSettings : _shipSettings).WithoutInjection();
        }
    }
}
