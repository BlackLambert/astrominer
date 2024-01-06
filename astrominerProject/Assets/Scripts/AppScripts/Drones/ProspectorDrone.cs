using SBaier.DI;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace SBaier.Astrominer
{
	public class ProspectorDrone : FlyableObject
	{
		public event Action<ProspectorDrone> OnDone;
		
		public Asteroid Target => _settings.Target;
		public FlyTarget Origin => _settings.Origin;
		public FlyTarget ReturnLocation => _settings.ReturnLocation;

		private DroneArguments _settings;

		public override void Inject(Resolver resolver)
		{
			base.Inject(resolver);
			_settings = resolver.Resolve<DroneArguments>();
		}

		protected override void OnEnable()
		{
			base.OnEnable();
			FlyTo(new FlightPath(new List<FlyTarget>() { Origin ,Target, ReturnLocation }));
		}

		protected override void OnTargetReached()
		{
			base.OnTargetReached();
			OnDone?.Invoke(this);
		}
	}
}
