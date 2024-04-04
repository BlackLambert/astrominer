using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class PlayersValueCalculatorCreator : MonoBehaviour, Injectable
    {
        private Players _players;
        private Resolver _resolver;
        private Pool<PlayerValueCalculator, Player> _pool;

        public void Inject(Resolver resolver)
        {
            _players = resolver.Resolve<Players>();
            _pool = resolver.Resolve<Pool<PlayerValueCalculator, Player>>();
        }

        private void Start()
        {
            CreateCalculators();
        }

        private void CreateCalculators()
        {
            foreach (Player player in _players)
            {
                PlayerValueCalculator calculator = _pool.Request(player);
                calculator.transform.SetParent(transform);
            }
        }
    }
}
