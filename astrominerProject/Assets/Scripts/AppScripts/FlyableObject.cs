using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public abstract class FlyableObject : MonoBehaviour, Flyable, Injectable, Initializable, Cleanable
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

        public virtual void Initialize()
        {
            _mover.OnTargetReached += OnTargetReached;
        }

        public virtual void Clean()
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
            Location.Value = flyTarget;
        }
    }
}
