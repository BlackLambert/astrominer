using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class OreSellingInstaller : MonoInstaller
    {
        [SerializeField]
        private OresSellingSettings _oresSellingSettings;

        public override void InstallBindings(Binder binder)
        {
            binder.BindToNewSelf<OreBank>();
            binder.BindInstance(_oresSellingSettings);
        }
    }
}
