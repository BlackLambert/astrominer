using System;

namespace SBaier.Astrominer
{
    public interface Flyable
    {
        FlyTarget FlyTarget { get; }
        event Action OnFlyTargetReached;
        public void FlyTo(FlyTarget target);
    }
}
