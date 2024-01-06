using System.Collections.Generic;
using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class PlayerCreationPanelInstaller : MonoInstaller
    {
        public override void InstallBindings(Binder binder)
        {
            binder.Bind<ActiveItem<PlayerColorOption>>().ToNew<SelectedPlayerColor>().AsSingle();
            binder.Bind<ActiveItem<string>>().ToNew<ChosenPlayerName>().AsSingle();
            binder.BindToNewSelf<MatchmakingPlayerCreator>();
        }
    }
}
