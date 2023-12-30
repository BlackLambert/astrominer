using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class FlyableObject : MonoBehaviour, Flyable, Injectable
    {
        private Mover _mover;

        public Observable<FlyTarget> FlyTarget { get; } = new Observable<FlyTarget>();
        public Observable<FlyTarget> Location { get; } = new Observable<FlyTarget>();
        
        public Vector2 Position2D => transform.position;
        public bool IsFlying => !_mover.TargetReached;


        public virtual void Inject(Resolver resolver)
        {
            _mover = resolver.Resolve<Mover>();
        }

        protected virtual void OnEnable()
        {
            _mover.OnTargetReached += OnTargetReached;
        }

        protected virtual void OnDisable()
        {
            _mover.OnTargetReached -= OnTargetReached;
        }

        public void FlyTo(FlyTarget target)
        {
            _mover.SetMovementTarget(target.LandingPoint);
            Location.Value = null;
            FlyTarget.Value = target;
        }

        protected virtual void OnTargetReached()
        {
            FlyTarget flyTarget = FlyTarget.Value;
            FlyTarget.Value = null;
            Debug.Log($"Set Location to {flyTarget}");
            Location.Value = flyTarget;
        }
    }
}
