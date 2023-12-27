using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class PlayerColorSelectionItemInstaller : MonoInstaller, Injectable
    {
        [SerializeField]
        private Selectable _selectable;
        [SerializeField]
        private PlayerColorSelectionItem _item;
        private PlayerColorOption _colorOption;

        public void Inject(Resolver resolver)
        {
            _colorOption = resolver.Resolve<PlayerColorOption>();
        }

        public override void InstallBindings(Binder binder)
        {
            binder.BindInstance(_colorOption);
            binder.BindInstance(_colorOption.Color);
            binder.BindInstance(_item);
            binder.BindInstance(_selectable);
        }
    }
}
