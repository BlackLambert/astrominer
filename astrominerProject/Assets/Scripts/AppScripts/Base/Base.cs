using SBaier.DI;

namespace SBaier.Astrominer
{
    public class Base : CosmicObject, Injectable
    {
        private const int _baseIdOffset = 10000;
        
        public Player Player { get; private set; }

        public override int Id => _id;
        private int _id;

        public void Inject(Resolver resolver)
        {
            Player = resolver.Resolve<Player>();
            _id = Player.Number * _baseIdOffset;
        }

        public override bool IsValidFlightTargetFor(Ship ship)
        {
            return base.IsValidFlightTargetFor(ship) &&
                   IsAllowedFlightTargetFor(ship.Player);
        }

        public override bool IsAllowedFlightTargetFor(Player player)
        {
            return base.IsAllowedFlightTargetFor(player) && player == Player;
        }
    }
}