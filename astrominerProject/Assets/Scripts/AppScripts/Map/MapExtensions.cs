using System.Collections.Generic;
using System.Linq;

namespace SBaier.Astrominer
{
    public static class MapExtensions
    {
        public static bool HasUnidentifiedValuableAsteroid(this Map map, Player player)
        {
            return map.Asteroids.Value.Any(asteroid => asteroid.IsUnidentifiedFor(player) && !asteroid.Exploited);
        }

        public static IEnumerable<Asteroid> GetUnidentifiedValuableAsteroids(this Map map, Player player)
        {
            return map.Asteroids.Value.Where(asteroid => asteroid.IsUnidentifiedFor(player) && !asteroid.Exploited);
        }
    }
}