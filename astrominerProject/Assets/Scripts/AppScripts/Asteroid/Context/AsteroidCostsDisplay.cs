namespace SBaier.Astrominer
{
    public class AsteroidCostsDisplay : ItemPropertyDisplay<Asteroid>
    {
        protected override string GetText()
        {
            float costs = _item.HasExploitMachine ? _item.ExploitMachine.MaintenanceCosts : 0;
            return $"Costs: {costs} per second";
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            _item.OnExploitMachineChanged += SetText;
        }

        private void OnDisable()
        {
            _item.OnExploitMachineChanged -= SetText;
        }
    }
}
