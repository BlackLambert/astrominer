namespace SBaier.Astrominer
{
    public class AsteroidOwningPlayer : ItemPropertyDisplay<Asteroid>
    {
        protected override string GetText()
        {
            return _item.HasOwningPlayer ? $"Player: {_item.OwningPlayer.GetColoredDisplayText()}"  : "No owning player";
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
