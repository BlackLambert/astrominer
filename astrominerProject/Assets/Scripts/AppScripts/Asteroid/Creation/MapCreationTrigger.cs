using System.Collections;
using System.Collections.Generic;
using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public abstract class MapCreationTrigger : MonoBehaviour, Injectable
    {
        protected MapCreationContext _context;
        protected Observable<MapCreationState> _state;
        private Map _map;
        private MapFactory _mapFactory;

        public void Inject(Resolver resolver)
        {
            _context = resolver.Resolve<MapCreationContext>();
            _map = resolver.Resolve<Map>();
            _mapFactory = resolver.Resolve<MapFactory>();
            _state = resolver.Resolve<Observable<MapCreationState>>();
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
            
            _map.AsteroidArguments.Value = _mapFactory.CreateMap(_context.SelectedAsteroidsAmountOption);
        }
    }
}
