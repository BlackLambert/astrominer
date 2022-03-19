using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class AsteroidInstaller : MonoInstaller, Injectable
    {
        [SerializeField]
        private Selectable _selectable;
        [SerializeField]
        private Asteroid _asteroid;

        private Asteroid.Arguments _arguments;

        public void Inject(Resolver resolver)
        {
            _arguments = resolver.Resolve<Asteroid.Arguments>();
        }

        public override void InstallBindings(Binder binder)
        {
            binder.BindInstance(_selectable).WithoutInjection();
            binder.BindInstance(_asteroid).WithoutInjection();
            binder.BindInstance(_arguments).WithoutInjection();
        }
    }
}
