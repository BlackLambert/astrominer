using SBaier.DI;

namespace SBaier.Astrominer
{
    public class AsteroidOwningPlayer : ItemPropertyDisplay<Asteroid>, Cleanable
    {
        protected override string GetText()
        {
            return _item.HasOwningPlayer ? $"Player: {_item.OwningPlayer.GetColoredDisplayText()}"  : "No owning player";
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
