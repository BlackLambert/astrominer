using SBaier.DI;

namespace SBaier.Astrominer
{
	public class SelectedAsteroidContextPanelCreator : SelectedItemContextMenuCreator<Asteroid, AsteroidContextPanel.Arguments>
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

		protected override AsteroidContextPanel.Arguments CreateArgument()
		{
			return new AsteroidContextPanel.Arguments()
			{
				Asteroid = _selectedItem.Value,
				Ship = _activeShip.Value
			};
		}
	}
}
