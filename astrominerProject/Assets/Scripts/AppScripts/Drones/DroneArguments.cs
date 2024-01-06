using UnityEngine;

namespace SBaier.Astrominer
{
    public class DroneArguments
    {
        public FlyTarget Origin { get; }
        public Asteroid Target { get; }
        public FlyTarget ReturnLocation { get; }
        public Player Player { get; }

        public DroneArguments(
            FlyTarget origin,
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
