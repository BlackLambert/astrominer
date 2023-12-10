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
            binder.Bind<Factory<AstroidsAmountSelectionItem, AstroidAmountOption>>()
                .ToNew<PrefabFactory<AstroidsAmountSelectionItem, AstroidAmountOption>>()
                .WithArgument(_item);
            binder.Bind<Pool<AstroidsAmountSelectionItem, AstroidAmountOption>>().
                ToNew<MonoPool<AstroidsAmountSelectionItem, AstroidAmountOption>>().
                WithArgument(_item).AsSingle();
            binder.BindToNewSelf<Selection>().AsSingle();
        }
    }
}
