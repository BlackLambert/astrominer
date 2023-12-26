using UnityEngine;

namespace SBaier.Astrominer
{
    public class DroneArguments
    {
        public Vector2 Origin { get; }
        public Asteroid Target { get; }
        public FlyTarget ReturnLocation { get; }
        public Player Player { get; }

        public DroneArguments(
            Vector2 origin,
            Asteroid target,
            FlyTarget returnLocation,
            Player player)
		{
            Origin = origin;
            Target = target;
            ReturnLocation = returnLocation;
            Player = player;
        }
    }
}
