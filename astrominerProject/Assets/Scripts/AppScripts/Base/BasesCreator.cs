using System.Collections.Generic;
using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class BasesCreator : MonoBehaviour, Injectable
    {
        private Bases _bases;
        private Pool<Base, Player> _pool;
        private BasePositions _positions;
        
        public void Inject(Resolver resolver)
        {
            _bases = resolver.Resolve<Bases>();
            _pool = resolver.Resolve<Pool<Base, Player>>();
            _positions = resolver.Resolve<BasePositions>();
        }

        private void OnEnable()
        {
            CreateBases();
        }

        private void CreateBases()
        {
            _bases.Clear();
            foreach (KeyValuePair<Player, Vector2> pair in _positions)
            {
                Base newBase = _pool.Request(pair.Key);
                newBase.transform.position = pair.Value;
                _bases.Add(pair.Key, newBase);
            }
        }
    }
}
