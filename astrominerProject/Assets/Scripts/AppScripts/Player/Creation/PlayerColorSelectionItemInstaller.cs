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
        private Color _color;

        public void Inject(Resolver resolver)
        {
            _color = resolver.Resolve<Color>();
        }

        public override void InstallBindings(Binder binder)
        {
            binder.BindInstance(_color);
            binder.BindInstance(_item);
            binder.BindInstance(_selectable);
        }
    }
}
