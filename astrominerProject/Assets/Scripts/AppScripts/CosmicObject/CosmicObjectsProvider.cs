using System.Collections.Generic;
using SBaier.DI;

namespace SBaier.Astrominer
{
    public class CosmicObjectsProvider : Provider<IList<CosmicObject>>, Injectable
    {
        public IList<CosmicObject> Value => GetList();
        
        private List<CosmicObject> _list = new List<CosmicObject>();

        private Map _map;
        private Bases _bases;
        
        public void Inject(Resolver resolver)
        {
            _map = resolver.Resolve<Map>();
            _bases = resolver.Resolve<Bases>();
        }

        private IList<CosmicObject> GetList()
        {
            _list.Clear();
            _list.AddRange(_map.Asteroids.Value);
            _list.AddRange(_bases.Values);
            return _list;
        }
    }
}
