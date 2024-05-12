using System;
using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class UpdateAsteroidPositionsOnAmountSelected : MapCreationTrigger, Initializable, Cleanable
    {
        public void Initialize()
        {
            CreateMap(_context.SelectedAsteroidsAmountOption);
            _context.SelectedAsteroidsAmountOption.OnValueChanged += OnAsteroidAmountOptionChanged;
        }

        public void Clean()
        {
            _context.SelectedAsteroidsAmountOption.OnValueChanged -= OnAsteroidAmountOptionChanged;
        }

        private void OnAsteroidAmountOptionChanged(AsteroidAmountOption formervalue, AsteroidAmountOption newvalue)
        {
            CreateMap(newvalue);
        }
    }
}
