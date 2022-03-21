using UnityEngine;

namespace SBaier.Astrominer
{
    public class DroneArguments
    {
        public Vector2 Origin { get; private set; }
        public Asteroid Target { get; private set; }
        public FlyTarget ReturnLocation { get; set; }

        public DroneArguments(
            Vector2 origin,
            Asteroid target,
            FlyTarget returnLocation)
		{
            Origin = origin;
            Target = target;
            ReturnLocation = returnLocation;
        }
    }
}
