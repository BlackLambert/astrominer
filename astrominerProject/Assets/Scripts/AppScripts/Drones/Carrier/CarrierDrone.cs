using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class CarrierDrone : Drone
    {
        public Ores CollectedOres { get; private set; }
        
        public override void Inject(Resolver resolver)
        {
            base.Inject(resolver);
            CollectedOres = resolver.Resolve<Ores>();
        }
    }
}
