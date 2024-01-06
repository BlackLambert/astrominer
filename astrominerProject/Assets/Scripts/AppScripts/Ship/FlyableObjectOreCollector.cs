using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
	public class FlyableObjectOreCollector : MonoBehaviour, Injectable
	{
		private OreCarrier _carrier;

		public void Inject(Resolver resolver)
		{
			_carrier = resolver.Resolve<OreCarrier>();
		}

		private void OnEnable()
		{
			AddListeners(_carrier.FlyTarget.Value);
			_carrier.FlyTarget.OnValueChanged += OnFlyTargetChanged;
		}

		private void OnDisable()
		{
			RemoveListeners(_carrier.FlyTarget.Value);
			_carrier.FlyTarget.OnValueChanged -= OnFlyTargetChanged;
		}

		private void OnFlyTargetChanged(FlightPath formervalue, FlightPath newvalue)
		{
			RemoveListeners(formervalue);
			AddListeners(newvalue);
		}

		private void AddListeners(FlightPath flyTargetValue)
		{
			if (flyTargetValue == null)
			{
				return;
			}

			flyTargetValue.OnIntermediateTargetReached += OnTargetReached;
			flyTargetValue.OnFinished += OnTargetReached;
		}

		private void RemoveListeners(FlightPath flyTargetValue)
		{
			if (flyTargetValue == null)
			{
				return;
			}

			flyTargetValue.OnIntermediateTargetReached -= OnTargetReached;
			flyTargetValue.OnFinished -= OnTargetReached;
		}

		private void OnTargetReached(FlyTarget flyTarget)
		{
			if (flyTarget is Asteroid asteroid &&
			    asteroid.OwningPlayer == _carrier.Player)
			{
				CollectOre(asteroid);
			}
		}

		private void CollectOre(Asteroid asteroid)
		{
			_carrier.CollectedOres.Add(asteroid.Collect());
		}
	}
}
