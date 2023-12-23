using SBaier.DI;

namespace SBaier.Astrominer
{
    public class PointerInputInstaller : MonoInstaller
    {
        private const int MOUSE_KEYS_AMOUNT = 3;

        public override void InstallBindings(Binder binder)
        {
            for (int i = 0; i < MOUSE_KEYS_AMOUNT; i++)
            {
#if UNITY_EDITOR || UNITY_STANDALONE
                binder.Bind<PointerInput>(i)
                    .And<MouseInput>(i)
                    .ToNew<MouseInput>()
                    .AsSingle();
                binder.BindComponent<MouseInputUpdater>(i)
                    .FromNewComponentOnNewGameObject($"PointerInputUpdater{i}", transform)
                    .WithArgument(new MouseInputUpdater.Arguments() { Id = i })
                    .AsNonResolvable();
#else
                throw new NotImplementedException();
#endif
            }
        }
    }
}