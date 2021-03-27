using UnityEngine;
using Zenject;

namespace Astrominer
{
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<CurrentSelectionRepository>().To<BasicCurrentSelectionRepository>().AsSingle();

        }
    }
}