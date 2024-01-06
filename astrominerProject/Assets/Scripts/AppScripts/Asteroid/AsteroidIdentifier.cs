using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class AsteroidIdentifier : MonoBehaviour, Injectable
    {
        private Flyable _flyable;
        private IdentifiedAsteroids _asteroids;

        public void Inject(Resolver resolver)
        {
            _flyable = resolver.Resolve<Flyable>();
            _asteroids = resolver.Resolve<IdentifiedAsteroids>();
        }

        private void OnEnable()
        {
            AddListeners(_flyable.FlyTarget.Value);
            _flyable.FlyTarget.OnValueChanged += OnFlyTargetChanged;
        }

        private void OnDisable()
        {
            RemoveListeners(_flyable.FlyTarget.Value);
            _flyable.FlyTarget.OnValueChanged -= OnFlyTargetChanged;
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
            if (flyTarget is Asteroid asteroid && !_asteroids.Contains(asteroid))
                _asteroids.Add(asteroid);
        }
    }
}