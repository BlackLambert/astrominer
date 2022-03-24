using SBaier.DI;

namespace SBaier.Astrominer
{
	public class Ship : FlyableObject
	{
		private ShipSettings _settings;

		public float Range => _settings.ActionRadius;
		public ObservableList<ExploitMachine> Machines { get; private set; } =
			new ObservableList<ExploitMachine>();

		public override void Inject(Resolver resolver)
		{
			base.Inject(resolver);
			_settings = resolver.Resolve<ShipSettings>();
		}

	}
}