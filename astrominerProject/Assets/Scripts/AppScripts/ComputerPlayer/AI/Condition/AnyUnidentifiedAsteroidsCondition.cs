using SBaier.AI;

namespace SBaier.Astrominer
{
    public class AnyUnidentifiedAsteroidsCondition : Node
    {
        private readonly Player _player;
        private Map _map;

        public AnyUnidentifiedAsteroidsCondition(Player player, Map map)
        {
            _player = player;
            _map = map;
        }
        
        public override bool Execute()
        {
            return _map.HasUnidentifiedAsteroid(_player);
        }
    }
}