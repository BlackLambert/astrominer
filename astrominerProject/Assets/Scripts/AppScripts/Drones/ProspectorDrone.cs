using SBaier.DI;
using System;
using UnityEngine;

namespace SBaier.Astrominer
{
	public class ProspectorDrone : FlyableObject
	{
		private DroneArguments _settings;
		public Asteroid Target => _settings.Target;
		public Vector2 Origin => _settings.Origin;
		public FlyTarget ReturnLocation => _settings.ReturnLocation;

		public event Action OnDone;

		public override void Inject(Resolver resolver)
		{
			base.Inject(resolver);
			_settings = resolver.Resolve<DroneArguments>();
		}

		protected override void Start()
		{
			base.Start();
			ChooseNextTarget();
			OnFlyTargetReached += ChooseNextTarget;
		}

		protected override void OnDestroy()
		{
			base.OnDestroy();
			OnFlyTargetReached -= ChooseNextTarget;
		}

		private void ChooseNextTarget()
		{
			if (FlyTarget == null)
				FlyTo(Target);
			else if (FlyTarget.Equals(Target))
				FlyTo(ReturnLocation);
			else
				OnDone?.Invoke();
		}

	}
}
