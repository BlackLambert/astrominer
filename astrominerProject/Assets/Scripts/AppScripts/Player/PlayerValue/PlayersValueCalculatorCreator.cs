using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class PlayersValueCalculatorCreator : MonoBehaviour, Injectable
    {
        private Players _players;
        private Resolver _resolver;
        private Pool<PlayerValueCalculator, Player, PrefabInstantiationArguments> _pool;

        public void Inject(Resolver resolver)
        {
            _players = resolver.Resolve<Players>();
            _pool = resolver.Resolve<Pool<PlayerValueCalculator, Player, PrefabInstantiationArguments>>();
        }

        private void Start()
        {
            CreateCalculators();
        }

        private void CreateCalculators()
        {
            foreach (Player player in _players)
            {
                _pool.Request(player, new PrefabInstantiationArguments() { Parent = transform });
            }
        }
    }
}