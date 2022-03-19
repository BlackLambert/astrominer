using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
	public class Ship : MonoBehaviour, Injectable
	{
		private ShipSettings _settings;
		private Mover _mover;

		public Vector2 Position2D => transform.position;
		public float Range => _settings.ActionRadius;
		public FlyTarget FlyTarget { get; private set; }

		public void Inject(Resolver resolver)
		{
			_settings = resolver.Resolve<ShipSettings>();
			_mover = resolver.Resolve<Mover>();
		}

		private void Start()
		{
			_mover.SpeedPerSeconds = _settings.SpeedPerSecond;
		}

		public void FlyTo(FlyTarget target)
		{
			_mover.SetMovementTarget(target.LandingPoint);
			FlyTarget = target;
		}
	}
}