using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class DronesDestructorCreator : MonoBehaviour, Injectable, Initializable
    {
        [SerializeField] 
        private Transform _hook;

        private Players _players;
        private Pool<DronesDestructor, Player, PrefabInstantiationArguments> _pool;
        
        public void Inject(Resolver resolver)
        {
            _players = resolver.Resolve<Players>();
            _pool = resolver.Resolve<Pool<DronesDestructor, Player, PrefabInstantiationArguments>>();
        }

        public void Initialize()
        {
            Create();
        }

        private void Create()
        {
            foreach (Player player in _players)
            {
                _pool.Request(player, new PrefabInstantiationArguments(){Parent = _hook});
            }
        }
    }
}
