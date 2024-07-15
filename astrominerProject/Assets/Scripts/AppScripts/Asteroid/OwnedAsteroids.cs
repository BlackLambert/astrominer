using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class OwnedAsteroids : ObservableList<Asteroid>
    {
        public float GetExploitedCosts()
        {
            return GetExploited().Sum(asteroid => asteroid.GetTotalCosts());
        }

        public float GetExploitedAmount()
        {
            return GetExploited().Count();
        }
        
        public IEnumerable<Asteroid> GetExploited()
        {
            return _items.Where(asteroid => asteroid.Exploited);
        }
    }
}
