using System.Collections.Generic;
using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class BasesCreator : MonoBehaviour, Injectable
    {
        private Map _map;
        private Pool<Base, Player> _pool;
        
        public void Inject(Resolver resolver)
        {
            _map = resolver.Resolve<Map>();
            _pool = resolver.Resolve<Pool<Base, Player>>();
        }

        private void OnEnable()
        {
            CreateBases();
        }

        private void CreateBases()
        {
            Dictionary<Player, Base> result = new Dictionary<Player, Base>();
            
            foreach (KeyValuePair<Player, Vector2> pair in _map.BasePositions.Value)
            {
                Base newBase = _pool.Request(pair.Key);
                newBase.transform.position = pair.Value;
                result.Add(pair.Key, newBase);
            }

            _map.Bases.Value = result;
        }
    }
}
