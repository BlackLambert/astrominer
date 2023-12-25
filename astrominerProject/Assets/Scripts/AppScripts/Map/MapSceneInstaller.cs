using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class MapSceneInstaller : MonoInstaller
    {
        [SerializeField]
        private AsteroidSettings _asteroidSettings;

        public override void InstallBindings(Binder binder)
        {
            binder.BindInstance(_asteroidSettings)
                .WithInjection();
            
            binder.BindToNewSelf<Map>()
                .AsSingle();

            binder.BindToNewSelf<Bases>()
                .AsSingle();

            binder.BindToNewSelf<BasePositions>()
                .AsSingle();
        }
    }
}
