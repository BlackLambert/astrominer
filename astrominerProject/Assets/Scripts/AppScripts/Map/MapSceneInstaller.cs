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

            /*binder.BindToNewSelf<Selection>()
                .AsSingle();

            binder.Bind<ActiveItem<Asteroid>>()
                .ToNew<SelectedAsteroid>()
                .AsSingle();*/
            
            binder.BindToNewSelf<Map>()
                .AsSingle();
        }
    }
}
