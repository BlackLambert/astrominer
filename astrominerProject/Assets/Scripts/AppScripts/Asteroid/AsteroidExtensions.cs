using System.Linq;

namespace SBaier.Astrominer
{
    public static class AsteroidExtensions
    {
        public static bool IsUnidentifiedFor(this Asteroid asteroid, Player player)
        {
            return !asteroid.HasOwningPlayer &&
                   !player.IdentifiedAsteroids.Contains(asteroid) &&
                   player.Drones.All(drone => drone.Target != asteroid);
        }
    }
}