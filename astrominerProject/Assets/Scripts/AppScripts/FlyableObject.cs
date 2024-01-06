using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class FlyableObject : MonoBehaviour, Flyable, Injectable
    {
        private FlightPathMover _mover;

        public Observable<FlightPath> FlyTarget { get; } = new Observable<FlightPath>();
        public Observable<FlyTarget> Location { get; } = new Observable<FlyTarget>();
        
        public Vector2 Position2D => transform.position;
        public bool IsFlying => !_mover.TargetReached;


        public virtual void Inject(Resolver resolver)
        {
            _mover = resolver.Resolve<FlightPathMover>();
        }

        protected virtual void OnEnable()
        {
            _mover.OnTargetReached += OnTargetReached;
        }

        protected virtual void OnDisable()
        {
            _mover.OnTargetReached -= OnTargetReached;
        }

        public void FlyTo(FlightPath path)
        {
            _mover.Move(path);
            Location.Value = null;
            FlyTarget.Value = path;
        }

        protected virtual void OnTargetReached()
        {
            FlyTarget flyTarget = FlyTarget.Value.LastTarget;
            FlyTarget.Value = null;
            Debug.Log($"Set Location to {flyTarget}");
            Location.Value = flyTarget;
        }
    }
}
