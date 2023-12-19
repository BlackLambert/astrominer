using PCGToolkit.Sampling;
using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class MapCreationInstaller : MonoInstaller, Injectable
    {
        [SerializeField]
        private MapCreationSettings _mapCreationSettings;

        [SerializeField] 
        private BasePlacementPreview _basePlacementPreviewPrefab;

        private System.Random _random;

        public void Inject(Resolver resolver)
        {
            _random = resolver.Resolve<System.Random>();
        }
        
        public override void InstallBindings(Binder binder)
        {
            binder.BindInstance(CreateSampler())
                .WithoutInjection();

            binder.BindInstance(_mapCreationSettings)
                .WithInjection();

            binder.BindToNewSelf<MapCreationContext>()
                .AsSingle();

            binder.Bind<ActiveItem<AstroidAmountOption>>()
                .ToNew<SelectedAstroidsAmount>()
                .AsSingle();

            binder.BindToNewSelf<AsteroidPositionsGenerator>().
                AsSingle();

            binder.BindToNewSelf<BasePlacementContext>()
                .AsSingle();
        }

        private PoissonDiskSampling2D CreateSampler()
        {
            return new PoissonDiskSampling2D(_random, _mapCreationSettings.MaxSamplerTries, new PoissonDiskSampling2DParameterValidator());
        }
    }
}
