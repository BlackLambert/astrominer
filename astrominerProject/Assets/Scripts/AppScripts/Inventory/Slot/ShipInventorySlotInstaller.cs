using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class ShipInventorySlotInstaller : MonoInstaller, Injectable
    {
        [SerializeField] 
        private ShipInventorySlot _slot;

        private ShipInventorySlot.Arguments _arguments;

        public void Inject(Resolver resolver)
        {
            _arguments = resolver.Resolve<ShipInventorySlot.Arguments>();
        }
        
        public override void InstallBindings(Binder binder)
        {
            binder.BindInstance(_slot).WithoutInjection();
            binder.BindInstance(_arguments).WithoutInjection();
        }
    }
}
