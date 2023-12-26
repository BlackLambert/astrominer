using SBaier.DI;
using System;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class FlyableObject : MonoBehaviour, Flyable, Injectable
    {
        private Mover _mover;
        private FlyTarget _flyTarget;
        public FlyTarget FlyTarget 
        {
            get => _flyTarget;
            private set
			{
                _flyTarget = value;
                OnFlyTargetChanged?.Invoke();
            }
        }
        public Vector2 Position2D => transform.position;
        public bool IsFlying => !_mover.TargetReached;

        public event Action OnFlyTargetReached;
        public event Action OnFlyTargetChanged;


        public virtual void Inject(Resolver resolver)
        {
            _mover = resolver.Resolve<Mover>();
        }

        protected virtual void OnEnable()
        {
            _mover.OnTargetReached += InvokeOnFlyTargetReached;
        }

        protected virtual void OnDisable()
        {
            _mover.OnTargetReached -= InvokeOnFlyTargetReached;
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
