using SBaier.DI;

namespace SBaier.Astrominer
{
    public class AsteroidCostsDisplay : ItemPropertyDisplay<Asteroid>, Cleanable
    {
        protected override string GetText()
        {
            float costs = _item.HasExploitMachine ? _item.ExploitMachine.MaintenanceCosts : 0;
            return $"Costs: {costs} per second";
        }

        public override void Initialize()
        {
            base.Initialize();
            _item.OnExploitMachineChanged += SetText;
        }

        public void Clean()
        {
            _item.OnExploitMachineChanged -= SetText;
        }
    }
}
