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

		protected override void OnEnable()
		{
			base.OnEnable();
			_identifiedAsteroids.OnItemAdded += CheckButtonActive;
		}

		protected override void OnDisable()
		{
			base.OnDisable();
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
