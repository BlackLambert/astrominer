using SBaier.AI;

namespace SBaier.Astrominer
{
    public class CanPurchaseCondition : Node
    {
        private readonly Player _player;
        private readonly float _price;

        public CanPurchaseCondition(Player player, float price)
        {
            _player = player;
            _price = price;
        }
        
        public override bool Execute()
        {
            return _player.Credits.Has(_price);
        }
    }
}