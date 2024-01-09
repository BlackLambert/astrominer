using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
	public class OreCollector : MonoBehaviour, Injectable
	{
		private FlyableObject _flyableObject;
		private Ores _collectedOres;
		private Player _player;

		public void Inject(Resolver resolver)
		{
			_flyableObject = resolver.Resolve<FlyableObject>();
			_collectedOres = resolver.Resolve<Ores>();
			_player = resolver.Resolve<Player>();
		}

		private void OnEnable()
		{
			AddListeners(_flyableObject.FlyTarget.Value);
			_flyableObject.FlyTarget.OnValueChanged += OnFlyTargetChanged;
		}

		private void OnDisable()
		{
			RemoveListeners(_flyableObject.FlyTarget.Value);
			_flyableObject.FlyTarget.OnValueChanged -= OnFlyTargetChanged;
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
			    asteroid.OwningPlayer == _player)
			{
				CollectOre(asteroid);
			}
		}

		private void CollectOre(Asteroid asteroid)
		{
			_collectedOres.Add(asteroid.Collect());
		}
	}
}
