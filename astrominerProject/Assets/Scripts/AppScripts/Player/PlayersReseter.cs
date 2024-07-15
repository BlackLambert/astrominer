using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class PlayersReseter : MonoBehaviour, Injectable, Cleanable
    {
        private Players _players;
        
        public void Inject(Resolver resolver)
        {
            _players = resolver.Resolve<Players>();
        }

        public void Clean()
        {
            foreach (Player player in _players)
            {
                player.Reset();
            }
        }
    }
}
