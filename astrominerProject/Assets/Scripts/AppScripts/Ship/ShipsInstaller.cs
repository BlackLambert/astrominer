using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class ShipsInstaller : MonoInstaller
    {
        [SerializeField]
        private Ship _shipPrefab;

        public override void InstallBindings(Binder binder)
        {
			binder.Bind<Factory<Ship>>().ToNew<PrefabFactory<Ship>>().WithArgument(_shipPrefab);
            binder.BindToNewSelf<ActiveShip>().AsSingle();
            binder.BindToNewSelf<Ships>().AsSingle();
        }
    }
}
