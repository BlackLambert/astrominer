using SBaier.DI;
using SBaier.Process;
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
            binder.BindInstance(_visualsSettings)
                .WithoutInjection();
            
            binder.BindInstance(new System.Random())
                .WithoutInjection();
            
            binder.Bind<Factory<Player, PlayerFactory.Arguments>>()
                .ToNew<PlayerFactory>();
            
            binder.BindInstance(_camera)
                .WithoutInjection();
            
            binder.BindToNewSelf<CameraZoom>()
                .AsSingle();
            
            binder.BindComponent<CoroutineHelper>()
                .FromNewComponentOnNewGameObject("CoroutineHelper", transform)
                .AsSingle();
            
            binder.Bind<ProcessQueue>()
                .ToNew<BasicProcessQueue>()
                .AsSingle();

            binder.Bind<Observable<Process.Process>>()
                .And<ReadonlyObservable<Process.Process>>()
                .ToNew<Observable<Process.Process>>()
                .AsSingle();
            
            binder.Bind<CommandsEnqueuer>()
                .ToNew<BasicCommandsEnqueuer>()
                .AsSingle();
            
            binder.BindComponent<LoadingScreenProcessStarter>()
                .FromNewComponentOnNewGameObject("ProcessStarter", transform)
                .AsNonResolvable();
        }
    }
}
