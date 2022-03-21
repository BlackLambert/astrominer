using SBaier.DI;
using System;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class FlyableObject : MonoBehaviour, Flyable, Injectable
    {
        private Mover _mover;
        public FlyTarget FlyTarget { get; private set; }
        public Vector2 Position2D => transform.position;
        public bool IsFlying => !_mover.TargetReached;

        public event Action OnFlyTargetReached;


        public virtual void Inject(Resolver resolver)
        {
            _mover = resolver.Resolve<Mover>();
        }

        protected virtual void Start()
        {
            InitMover();
            _mover.OnTargetReached += InvokeOnFlyTargetReached;
        }

        protected virtual void OnDestroy()
        {
            _mover.OnTargetReached -= InvokeOnFlyTargetReached;
        }

        private void InitMover()
        {
            _mover.SetMovementTarget(Position2D);
        }

        public void FlyTo(FlyTarget target)
        {
            _mover.SetMovementTarget(target.LandingPoint);
            FlyTarget = target;
        }

        private void InvokeOnFlyTargetReached()
        {
            OnFlyTargetReached?.Invoke();
        }
    }
}
