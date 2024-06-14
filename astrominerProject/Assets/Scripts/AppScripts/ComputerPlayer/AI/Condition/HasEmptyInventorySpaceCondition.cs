using SBaier.AI;

namespace SBaier.Astrominer
{
    public class HasEmptyInventorySpaceCondition : Node
    {
        private readonly Ship _ship;

        public HasEmptyInventorySpaceCondition(Ship ship)
        {
            _ship = ship;
        }

        public override bool Execute()
        {
            return _ship.HasEmptyInventorySpace;
        }
    }
}
