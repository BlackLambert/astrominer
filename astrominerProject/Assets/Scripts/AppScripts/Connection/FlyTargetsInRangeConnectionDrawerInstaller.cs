using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class FlyTargetsInRangeConnectionDrawerInstaller : MonoInstaller, Injectable
    {
        [SerializeField] 
        private FlyTargetsInRangeDetector _detector;
        
        private FlyTargetsInRangeDetector.Arguments _arguments;

        public void Inject(Resolver resolver)
        {
            _arguments = resolver.Resolve<FlyTargetsInRangeDetector.Arguments>();
        }
        
        public override void InstallBindings(Binder binder)
        {
            binder.BindInstance(_arguments)
                .WithoutInjection();
            
            binder.Bind<InRangeDetector2D<FlyTarget>>()
                .ToInstance(_detector)
                .WithoutInjection();
        }
    }
}
