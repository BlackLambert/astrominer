using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class BootstrapInstaller : MonoInstaller
    {
        public override void InstallBindings(Binder binder)
        {
            binder.Bind<MonoBehaviour>(BindingIds.CoroutineHelper)
                .ToInstance(this)
                .WithoutInjection();
        }
    }
}
