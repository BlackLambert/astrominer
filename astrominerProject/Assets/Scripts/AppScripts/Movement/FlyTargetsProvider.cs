using System.Collections;
using System.Collections.Generic;
using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class FlyTargetsProvider : Provider<IList<FlyTarget>>, Injectable
    {
        public IList<FlyTarget> Value => GetList();
        
        private List<FlyTarget> _list = new List<FlyTarget>();

        private Map _map;
        private Bases _bases;
        
        public void Inject(Resolver resolver)
        {
            _map = resolver.Resolve<Map>();
            _bases = resolver.Resolve<Bases>();
        }

        private IList<FlyTarget> GetList()
        {
            _list.Clear();
            _list.AddRange(_map.Asteroids.Value);
            _list.AddRange(_bases.Values);
            return _list;
        }
    }
}
