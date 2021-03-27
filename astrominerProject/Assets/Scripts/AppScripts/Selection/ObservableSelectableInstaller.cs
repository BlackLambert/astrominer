using UnityEngine;
using Zenject;

namespace Astrominer
{
    public class ObservableSelectableInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind(typeof(ObservableSelectable), typeof(Selectable)).To<BasicObservableSelectable>().AsSingle();
        }
    }
}