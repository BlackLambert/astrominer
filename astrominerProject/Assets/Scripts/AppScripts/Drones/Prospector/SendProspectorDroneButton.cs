using SBaier.DI;

namespace SBaier.Astrominer
{
    public class SendProspectorDroneButton : SendDroneButton<ProspectorDrone>
    {
		private IdentifiedAsteroids _identifiedAsteroids;

		public override void Inject(Resolver resolver)
		{
			base.Inject(resolver);
			_identifiedAsteroids = resolver.Resolve<IdentifiedAsteroids>();
		}

		public override void Initialize()
		{
			base.Initialize();
			_identifiedAsteroids.OnItemAdded += CheckButtonActive;
		}

		public override void Clean()
		{
			base.Clean();
			_identifiedAsteroids.OnItemAdded -= CheckButtonActive;
		}

		private void CheckButtonActive(Asteroid _)
		{
			UpdateButtonActive();
		}

		protected override bool GetButtonActive()
		{
			return base.GetButtonActive() &&
				!_identifiedAsteroids.Contains(_target);
		}
	}
}
