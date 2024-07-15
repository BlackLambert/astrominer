using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class PlayerValuesInstaller : MonoInstaller, Injectable
    {
        [SerializeField] private PlayerValueSettings _settings;
        
        private Players _players;

        public void Inject(Resolver resolver)
        {
            _players = resolver.Resolve<Players>();
        }
        
        public override void InstallBindings(Binder binder)
        {
            binder.BindInstance(CreatePlayerValues()).WithoutInjection();
            binder.BindInstance(_settings).WithoutInjection();
        }

        private PlayerValues CreatePlayerValues()
        {
            PlayerValues playerValues = new PlayerValues(_settings.ValueHistoryBufferSize);
            foreach (Player player in _players)
            {
                playerValues.AddNewValueFor(player);
            }

            return playerValues;
        }
    }
}
