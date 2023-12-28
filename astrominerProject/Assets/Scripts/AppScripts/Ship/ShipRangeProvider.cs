using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class ShipRangeProvider : Provider<float>
    {
        private Ship _ship;
        
        public ShipRangeProvider(Ship ship)
        {
            _ship = ship;
        }

        public float Value => _ship.Range;
    }
}
