using SBaier.DI;
using System.Collections;
using System.Collections.Generic;
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
