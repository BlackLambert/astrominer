using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class PopupInstaller : MonoInstaller
    {
        [SerializeField] 
        private Popup _popup;
        
        public override void InstallBindings(Binder binder)
        {
            binder.BindInstance(_popup).WithoutInjection();
        }
    }
}
