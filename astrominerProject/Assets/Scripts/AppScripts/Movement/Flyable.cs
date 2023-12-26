using System;

namespace SBaier.Astrominer
{
    public interface Flyable
    {
        FlyTarget FlyTarget { get; }
        FlyTarget Location { get; }
        event Action OnFlyTargetReached;
        event Action OnLocationChanged;
        public void FlyTo(FlyTarget target);
    }
}
