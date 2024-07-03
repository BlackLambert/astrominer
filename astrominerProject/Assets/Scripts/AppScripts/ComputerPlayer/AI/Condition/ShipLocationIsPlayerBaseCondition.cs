using SBaier.AI;

namespace SBaier.Astrominer
{
    public class ShipLocationIsPlayerBaseCondition : Node
    {
        private readonly Ship _ship;

        public ShipLocationIsPlayerBaseCondition(Ship ship)
        {
            _ship = ship;
        }
        
        public override bool Execute()
        {
            return _ship.Location.Value is Base playerBase && playerBase.Player == _ship.Player;
        }
    }
}