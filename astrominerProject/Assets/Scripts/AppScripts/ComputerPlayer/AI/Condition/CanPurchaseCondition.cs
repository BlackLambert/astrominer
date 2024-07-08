using SBaier.AI;

namespace SBaier.Astrominer
{
    public class CanPurchaseCondition : NodeBase
    {
        private readonly AIBrain _brain;
        private readonly float _price;

        public CanPurchaseCondition(AIBrain brain, float price)
        {
            _brain = brain;
            _price = price;
        }
        
        public override bool Execute()
        {
            return _brain.CanAfford(_price);
        }
    }
}