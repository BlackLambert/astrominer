using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class AIInstaller : MonoInstaller
    {
        [SerializeField] 
        private AISettings _settings;
        
        public override void InstallBindings(Binder binder)
        {
            binder.BindInstance(_settings);
        }
    }
}