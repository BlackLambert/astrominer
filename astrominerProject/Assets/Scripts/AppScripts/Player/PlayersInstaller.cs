using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class PlayersInstaller : MonoInstaller
    {
        [SerializeField]
        private PlayerSettings _playerSettings;

        public override void InstallBindings(Binder binder)
        {
            binder.BindToNewSelf<Players>().AsSingle();
            binder.BindInstance(_playerSettings);
        }
    }
}
