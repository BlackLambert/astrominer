using System;
using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class UpdateAsteroidPositionsOnAmountSelected : MonoBehaviour, Injectable
    {
        private MapCreationContext _context;
        private Map _map;
        private MapCreator _mapCreator;

        public void Inject(Resolver resolver)
        {
            _context = resolver.Resolve<MapCreationContext>();
            _map = resolver.Resolve<Map>();
            _mapCreator = resolver.Resolve<MapCreator>();
        }

        private void OnEnable()
        {
            _context.SelectedAsteroidsAmountOption.OnValueChanged += OnAsteroidAmountOptionChanged;
        }

        private void OnDisable()
        {
            _context.SelectedAsteroidsAmountOption.OnValueChanged -= OnAsteroidAmountOptionChanged;
        }

        private void OnAsteroidAmountOptionChanged(AsteroidAmountOption formervalue, AsteroidAmountOption newvalue)
        {
            if (newvalue == null)
            {
                return;
            }
            
            _map.AsteroidArguments.Value = _mapCreator.CreateMap();
        }
    }
}
