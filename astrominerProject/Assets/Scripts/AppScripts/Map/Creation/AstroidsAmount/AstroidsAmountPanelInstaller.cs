using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class AstroidsAmountPanelInstaller : MonoInstaller
    {
        [SerializeField]
        private AstroidsAmountSelectionItem _item;

        public override void InstallBindings(Binder binder)
        {
            binder.Bind<Factory<AstroidsAmountSelectionItem, AsteroidAmountOption>>()
                .ToNew<PrefabFactory<AstroidsAmountSelectionItem, AsteroidAmountOption>>()
                .WithArgument(_item);
            binder.Bind<Pool<AstroidsAmountSelectionItem, AsteroidAmountOption>>().
                ToNew<MonoPool<AstroidsAmountSelectionItem, AsteroidAmountOption>>().
                WithArgument(_item).AsSingle();
            binder.BindToNewSelf<Selection>().AsSingle();
        }
    }
}
