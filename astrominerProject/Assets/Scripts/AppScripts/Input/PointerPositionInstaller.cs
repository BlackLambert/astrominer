using SBaier.DI;

namespace SBaier.Astrominer
{
    public class PointerPositionInstaller : MonoInstaller
    {
        public override void InstallBindings(Binder binder)
        {
            #if UNITY_EDITOR || UNITY_STANDALONE
            binder.Bind<PointerPosition>()
                .ToNew<MousePointerPosition>()
                .AsSingle();
            #elif UNITY_IOS || UNITY_ANDROID
            binder.Bind<PointerPosition>()
                .ToNew<TouchPointerPosition>()
                .AsSingle();
            #else
            throw new NotImplementedException();
            #endif
        }
    }
}
