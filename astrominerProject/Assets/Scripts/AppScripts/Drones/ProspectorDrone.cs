using SBaier.DI;
using System;
using UnityEngine;

namespace SBaier.Astrominer
{
	public class ProspectorDrone : FlyableObject
	{
		public event Action<ProspectorDrone> OnDone;
		
		public Asteroid Target => _settings.Target;
		public Vector2 Origin => _settings.Origin;
		public FlyTarget ReturnLocation => _settings.ReturnLocation;

		private DroneArguments _settings;
		private bool _targetReached;

		public override void Inject(Resolver resolver)
		{
			base.Inject(resolver);
			_settings = resolver.Resolve<DroneArguments>();
		}

		protected override void OnEnable()
		{
			base.OnEnable();
			_targetReached = false;
			ChooseNextTarget();
		}

		protected override void OnTargetReached()
		{
			base.OnTargetReached();

			if (Location.Equals(Target))
			{
				_targetReached = true;
			}
			
			ChooseNextTarget();
		}

		private void ChooseNextTarget()
		{
			if (!_targetReached)
			{
				FlyTo(Target);
			}
			else if(Location.Equals(Target))
			{
				FlyTo(ReturnLocation);
			}
			else if(Location.Equals(ReturnLocation))
			{
				OnDone?.Invoke(this);
			}
		}

	}
}
