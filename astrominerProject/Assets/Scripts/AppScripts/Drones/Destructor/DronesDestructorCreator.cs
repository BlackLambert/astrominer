using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class DronesDestructorCreator : MonoBehaviour, Injectable
    {
        [SerializeField] 
        private Transform _hook;

        private Players _players;
        private Pool<DronesDestructor, Player> _pool;
        
        public void Inject(Resolver resolver)
        {
            _players = resolver.Resolve<Players>();
            _pool = resolver.Resolve<Pool<DronesDestructor, Player>>();
        }

        private void Start()
        {
            Create();
        }

        private void Create()
        {
            foreach (Player player in _players)
            {
                DronesDestructor destructor = _pool.Request(player);
                destructor.transform.SetParent(_hook, false);
            }
        }
    }
}
