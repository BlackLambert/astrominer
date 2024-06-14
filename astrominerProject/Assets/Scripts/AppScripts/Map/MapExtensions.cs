using System.Collections.Generic;
using System.Linq;

namespace SBaier.Astrominer
{
    public static class MapExtensions
    {
        public static bool HasUnidentifiedAsteroid(this Map map, Player player)
        {
            return map.Asteroids.Value.Any(asteroid => asteroid.IsUnidentifiedFor(player));
        }
        
        public static List<Asteroid> GetUnidentifiedAsteroids(this Map map, Player player)
        {
            return map.Asteroids.Value.Where(asteroid => asteroid.IsUnidentifiedFor(player)).ToList();
        }
    }
}