using SBaier.AI;

namespace SBaier.Astrominer
{
    public class ShipLocationIsEmptyAsteroidCondition : Node
    {
        private readonly Ship _ship;

        public ShipLocationIsEmptyAsteroidCondition(Ship ship)
        {
            _ship = ship;
        }
        
        public override bool Execute()
        {
            return _ship.Location.Value is Asteroid { HasOwningPlayer: false };
        }
    }
}