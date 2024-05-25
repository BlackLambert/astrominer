using SBaier.DI;
using SBaier.Process;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class BootstrapInstaller : MonoInstaller
    {
        public override void InstallBindings(Binder binder)
        {
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
            
            binder.BindComponent<BasicProcessStarter>()
                .FromNewComponentOnNewGameObject("ProcessStarter", transform)
                .AsNonResolvable();
            
            binder.Bind<CommandsEnqueuer>()
                .ToNew<BasicCommandsEnqueuer>()
                .AsSingle();
        }
    }
}
