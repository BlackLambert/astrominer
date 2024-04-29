using SBaier.DI;

namespace SBaier.Astrominer
{
    public class SceneChangeInstaller : MonoInstaller
    {
        public override void InstallBindings(Binder binder)
        {
            binder.BindComponent<CoroutineHelper>()
                .FromNewComponentOnNewGameObject("CoroutineHelper", transform);
            binder.Bind<CommandsExecutor>().ToNew<CommandsExecutorImpl>().AsSingle();
        }
    }
}