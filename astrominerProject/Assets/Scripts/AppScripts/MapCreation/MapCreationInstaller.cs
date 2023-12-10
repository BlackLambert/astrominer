using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class MapCreationInstaller : MonoInstaller
    {
        public override void InstallBindings(Binder binder)
        {

            binder.BindToNewSelf<MapCreationContext>()
                .AsSingle();

            binder.Bind<ActiveItem<AstroidAmountOption>>()
                .ToNew<SelectedAstroidsAmount>()
                .AsSingle();
        }
    }
}
