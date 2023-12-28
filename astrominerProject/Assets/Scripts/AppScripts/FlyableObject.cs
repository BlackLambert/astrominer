using SBaier.DI;
using System;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class FlyableObject : MonoBehaviour, Flyable, Injectable
    {
        private Mover _mover;
        private FlyTarget _flyTarget;
        private FlyTarget _location;
        
        public FlyTarget FlyTarget 
        {
            get => _flyTarget;
            private set
			{
                _flyTarget = value;
                OnFlyTargetChanged?.Invoke();
            }
        }

        public FlyTarget Location
        {
            get => _location;
            private set
            {
                _location = value;
                OnLocationChanged?.Invoke();
            }
        }
        
        public Vector2 Position2D => transform.position;
        public bool IsFlying => !_mover.TargetReached;

        public event Action OnFlyTargetReached;
        public event Action OnFlyTargetChanged;
        public event Action OnLocationChanged;


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
            Location = null;
            FlyTarget = target;
        }

        protected virtual void OnTargetReached()
        {
            FlyTarget flyTarget = _flyTarget;
            OnFlyTargetReached?.Invoke();
            FlyTarget = null;
            Debug.Log($"Set Location to {flyTarget}");
            Location = flyTarget;
        }
    }
}
