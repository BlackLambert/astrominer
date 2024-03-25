using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class OreValueGraphInstaller : MonoInstaller
    {
        [SerializeField] 
        private OreType _oreType;
        
        public override void InstallBindings(Binder binder)
        {
            binder.BindInstance(_oreType);
        }
    }
}
