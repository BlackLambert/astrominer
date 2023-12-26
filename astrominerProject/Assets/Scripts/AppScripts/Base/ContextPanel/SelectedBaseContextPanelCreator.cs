using SBaier.DI;

namespace SBaier.Astrominer
{
    public class SelectedBaseContextPanelCreator : SelectedItemContextMenuCreator<Base>
    {
        private ActiveItem<Ship> _activeShip;

        public override void Inject(Resolver resolver)
        {
            base.Inject(resolver);
            _activeShip = resolver.Resolve<ActiveItem<Ship>>();
        }

        protected override bool CanCreateContextPanel()
        {
            return base.CanCreateContextPanel() && _activeShip.HasValue;
        }
    }
}
