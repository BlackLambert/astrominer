using UnityEngine;
using Zenject;

namespace Astrominer
{
    public class AsteroidInstaller : MonoInstaller
    {
        [SerializeField]
        private Asteroid _asteroid;

        public override void InstallBindings()
        {
            Container.Bind<Asteroid>().FromInstance(_asteroid).AsSingle();
        }
    }
}