using System.Collections.Generic;
using System.Linq;

namespace SBaier.Astrominer
{
    public class IdentifiedAsteroids : ObservableList<Asteroid>
    {
        public float GetValueOfEmptyAsteroids()
        {
            return _items.Where(asteroid => !asteroid.HasOwningPlayer).Sum(emptyAsteroid => emptyAsteroid.Value);
        }

        public IEnumerable<Asteroid> GetEmptyValuableAsteroids()
        {
            return _items.Where(asteroid => !asteroid.HasOwningPlayer && !asteroid.Exploited);
        }
    }
}
