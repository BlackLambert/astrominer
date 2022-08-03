using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class PlayerCreationPanelInstaller : MonoInstaller
    {
        [SerializeField]
        private PlayerColorSelectionItem _colorSelectionItemPrefab;
        [SerializeField]
        private PlayerCreationPanel _panel;

        public override void InstallBindings(Binder binder)
        {
            binder.Bind<Factory<PlayerColorSelectionItem, Color>>().
                ToNew<PrefabFactory<PlayerColorSelectionItem, Color>>().
                WithArgument(_colorSelectionItemPrefab);
            binder.Bind<Pool<PlayerColorSelectionItem, Color>>().
                ToNew <MonoPool<PlayerColorSelectionItem, Color>>().
                WithArgument(_colorSelectionItemPrefab);
            binder.BindInstance(_panel);
            binder.Bind<ActiveItem<PlayerColorSelectionItem>>().ToNew<SelectedPlayerColor>().AsSingle();
            binder.Bind<ActiveItem<string>>().ToNew<ChosenPlayerName>().AsSingle();
            binder.BindToNewSelf<Selection>().AsSingle();
            binder.BindToNewSelf<MatchmakingPlayerCreator>();
        }
    }
}
