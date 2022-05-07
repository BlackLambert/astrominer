using SBaier.DI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class PlayerListItemInstaller : MonoInstaller, Injectable
    {
        private Player _player;

        public void Inject(Resolver resolver)
        {
            _player = resolver.Resolve<Player>();
        }

        public override void InstallBindings(Binder binder)
        {
            binder.BindInstance(_player);
            binder.BindInstance(_player.Color);
        }
    }
}
