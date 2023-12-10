using UnityEngine;
using SBaier.DI;

namespace SBaier.Astrominer
{
    public class AstroidsAmountSelectionItemInstaller : MonoInstaller, Injectable
    {
        [SerializeField]
        private Selectable _selectable;

        private AstroidAmountOption _option;

        public void Inject(Resolver resolver)
        {
            _option = resolver.Resolve<AstroidAmountOption>();
        }

        public override void InstallBindings(Binder binder)
        {
            binder.BindInstance(_option).WithoutInjection();
            binder.BindInstance(_selectable).WithoutInjection();
        }
    }
}
