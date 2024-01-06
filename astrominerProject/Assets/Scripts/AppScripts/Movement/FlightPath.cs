using System;
using System.Collections.Generic;

namespace SBaier.Astrominer
{
    public class FlightPath
    {
        public event Action<FlyTarget> OnFinished; 
        public event Action<FlyTarget> OnIntermediateTargetReached; 
        public IReadOnlyList<FlyTarget> FlyTargets => _flyTargets;
        public FlyTarget CurrentTarget => _flyTargets[_currentTargetIndex];
        public FlyTarget LastTarget => _flyTargets[^1];
        public bool Finished => _currentTargetIndex == _flyTargets.Count;
        public bool Canceled { get; private set; } = false;

        private List<FlyTarget> _flyTargets;
        private int _currentTargetIndex = 1;
        
        public FlightPath(List<FlyTarget> flyTargets)
        {
            if (flyTargets.Count == 0)
            {
                throw new ArgumentException("There has to be at least one fly target");
            }
            
            _flyTargets = flyTargets;
        }

        public void TargetNext()
        {
            if (Canceled)
            {
                throw new InvalidOperationException("The path has been canceled");
            }

            if (Finished)
            {
                throw new InvalidOperationException("The path is already finished");
            }
            
            OnIntermediateTargetReached?.Invoke(CurrentTarget);
            _currentTargetIndex++;

            if (Finished)
            {
                OnFinished?.Invoke(LastTarget);
            }
        }

        public void Cancel()
        {
            Canceled = true;
        }
    }
}