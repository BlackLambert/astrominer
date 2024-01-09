using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class SendDroneButtonInstaller : MonoInstaller
    {
        [SerializeField] 
        private DroneSettings _droneSettings;
        
        public override void InstallBindings(Binder binder)
        {
            binder.BindInstance(_droneSettings);
        }
    }
}
