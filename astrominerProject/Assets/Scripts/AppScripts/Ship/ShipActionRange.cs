using SBaier.DI;

namespace SBaier.Astrominer
{
    public class ShipActionRange : ActionRange, Injectable
    {
        public float Range => _ship.Range;

        private Ship _ship;
        
        public void Inject(Resolver resolver)
        {
            _ship = resolver.Resolve<Ship>();
        }
    }
}
