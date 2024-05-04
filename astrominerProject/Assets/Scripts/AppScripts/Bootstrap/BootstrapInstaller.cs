using SBaier.DI;
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
            
            binder.BindToNewSelf<ProcessQueue>()
                .AsSingle();

            binder.BindToNewSelf<Observable<Process>>();
            
            binder.BindComponent<BasicProcessStarter>()
                .FromNewComponentOnNewGameObject("ProcessStarter", transform)
                .AsNonResolvable();
            
            binder.Bind<CommandsEnqueuer>()
                .ToNew<BasicCommandsEnqueuer>()
                .AsSingle();
        }
    }
}
