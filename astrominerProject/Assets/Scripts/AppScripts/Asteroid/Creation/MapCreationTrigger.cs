using System.Collections;
using System.Collections.Generic;
using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public abstract class MapCreationTrigger : MonoBehaviour, Injectable
    {
        protected MapCreationContext _context;
        private Map _map;
        private MapCreator _mapCreator;

        public void Inject(Resolver resolver)
        {
            _context = resolver.Resolve<MapCreationContext>();
            _map = resolver.Resolve<Map>();
            _mapCreator = resolver.Resolve<MapCreator>();
        }

        protected void CreateMap()
        {
            CreateMap(_context.SelectedAsteroidsAmountOption);
        }

        protected void CreateMap(AsteroidAmountOption amountOption)
        {
            if (amountOption == null)
            {
                return;
            }
            
            _map.AsteroidArguments.Value = _mapCreator.CreateMap(_context.SelectedAsteroidsAmountOption);
        }
    }
}
