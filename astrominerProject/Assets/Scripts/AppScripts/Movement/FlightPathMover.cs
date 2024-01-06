using System;
using SBaier.DI;

namespace SBaier.Astrominer
{
    public class FlightPathMover : Injectable
    {
        public event Action OnTargetReached;
        public bool TargetReached => _path is { Finished: true };

        private FlightPath _path;
        private Mover _mover;
        
        public void Inject(Resolver resolver)
        {
            _mover = resolver.Resolve<Mover>();
        }

        public void Move(FlightPath path)
        {
            if (_path is { Canceled: false, Finished: false })
            {
                return;
            }

            if (_path != null)
            {
                _mover.OnTargetReached -= OnMoverTargetReached;
            }
            
            
            _mover.OnTargetReached += OnMoverTargetReached;
            _path = path;
            _mover.SetMovementTarget(_path.CurrentTarget.LandingPoint);
        }

        private void OnMoverTargetReached()
        {
            _path.TargetNext();

            if (_path.Finished)
            {
                _mover.OnTargetReached -= OnMoverTargetReached;
                _path = null;
                OnTargetReached?.Invoke();
            }
            else
            {
                _mover.SetMovementTarget(_path.CurrentTarget.LandingPoint);
            }
        }
    }
}