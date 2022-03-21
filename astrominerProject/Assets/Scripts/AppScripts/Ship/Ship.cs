using SBaier.DI;

namespace SBaier.Astrominer
{
	public class Ship : FlyableObject
	{
		private ShipSettings _settings;

		public float Range => _settings.ActionRadius;

		public override void Inject(Resolver resolver)
		{
			base.Inject(resolver);
			_settings = resolver.Resolve<ShipSettings>();
		}

	}
}