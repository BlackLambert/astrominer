using SBaier.DI;
using System;
using System.Collections.Generic;

namespace SBaier.Astrominer
{
	public abstract class Drone : FlyableObject
	{
		public event Action<Drone> OnDone;

		public bool TargetReached { get; private set; } = false;
		public Asteroid Target => _settings.Target;
		public FlyTarget Origin => _settings.Origin;
		public FlyTarget ReturnLocation => _settings.ReturnLocation;

		private DroneArguments _settings;

		public override void Inject(Resolver resolver)
		{
			base.Inject(resolver);
			_settings = resolver.Resolve<DroneArguments>();
		}

		public override void Initialize()
		{
			base.Initialize();
			FlyTo(new FlightPath(new List<FlyTarget>() { Origin ,Target, ReturnLocation }));
		}

		protected override void OnTargetReached()
		{
			base.OnTargetReached();
			TargetReached = true;
			OnDone?.Invoke(this);
		}
	}
}
