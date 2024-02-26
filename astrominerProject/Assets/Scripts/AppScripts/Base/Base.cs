using SBaier.DI;

namespace SBaier.Astrominer
{
    public class Base : CosmicObject, Injectable
    {
        public Player Player { get; private set; }

        public void Inject(Resolver resolver)
        {
            Player = resolver.Resolve<Player>();
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