using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class PlayerListPanelInstaller : MonoInstaller
    {
        [SerializeField]
        private PlayerListItem _listItemPrefab;

        public override void InstallBindings(Binder binder)
        {
            binder.Bind<Factory<PlayerListItem, Player>>().
                ToNew<PrefabFactory<PlayerListItem, Player>>().
                WithArgument(_listItemPrefab);
            binder.Bind<Pool<PlayerListItem, Player>>().
                ToNew<MonoPool<PlayerListItem, Player>>().
                WithArgument(_listItemPrefab);
        }
    }
}
