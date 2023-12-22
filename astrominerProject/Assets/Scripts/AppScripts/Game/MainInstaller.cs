using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class MainInstaller : MonoInstaller
    {
        [SerializeField]
        private VisualsSettings _visualsSettings;

        [SerializeField] 
        private Camera _camera;

        public override void InstallBindings(Binder binder)
        {
            binder.BindInstance(_visualsSettings).WithoutInjection();
            binder.BindInstance(new System.Random()).WithoutInjection();
            binder.Bind<Factory<Player, PlayerFactory.Arguments>>().ToNew<PlayerFactory>();
            binder.BindInstance(_camera).WithoutInjection();
        }
    }
}
