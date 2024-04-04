using SBaier.DI;

namespace SBaier.Astrominer
{
    public class PlayerValuesInstaller : MonoInstaller, Injectable
    {
        private Players _players;

        public void Inject(Resolver resolver)
        {
            _players = resolver.Resolve<Players>();
        }
        
        public override void InstallBindings(Binder binder)
        {
            PlayerValues playerValues = new PlayerValues();
            foreach (Player player in _players)
            {
                playerValues.AddNewValueFor(player);
            }
            binder.BindInstance(playerValues).WithoutInjection();
        }
    }
}
