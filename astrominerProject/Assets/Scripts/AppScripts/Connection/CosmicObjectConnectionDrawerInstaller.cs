using System.Collections.Generic;
using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class CosmicObjectConnectionDrawerInstaller : MonoInstaller, Injectable
    {
        [SerializeField] 
        private CosmicObjectInRangeDetector _detector;
        
        private MonoBehaviourInRangeDetector2D<CosmicObject>.Arguments _arguments;
        private Map _map;
        private Bases _bases;

        public void Inject(Resolver resolver)
        {
            _arguments = resolver.Resolve<MonoBehaviourInRangeDetector2D<CosmicObject>.Arguments>();
            _map = resolver.Resolve<Map>();
            _bases = resolver.Resolve<Bases>();
        }
        
        public override void InstallBindings(Binder binder)
        {
            binder.BindInstance(_arguments)
                .WithoutInjection();
            
            binder.Bind<MonoBehaviourInRangeDetector2D<CosmicObject>>()
                .ToInstance(_detector)
                .WithoutInjection();
            
            binder.Bind<Provider<IList<CosmicObject>>>()
                .To<BasicProvider<IList<CosmicObject>>>()
                .FromMethod(CreateProvider)
                .AsSingle();
        }

        private BasicProvider<IList<CosmicObject>> CreateProvider()
        {
            List<CosmicObject> list = new List<CosmicObject>();
            list.AddRange(_map.Asteroids.Value);
            list.AddRange(_bases.Values);
            return new BasicProvider<IList<CosmicObject>>(list);
        }
    }
}
