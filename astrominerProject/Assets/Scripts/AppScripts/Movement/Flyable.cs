using System;

namespace SBaier.Astrominer
{
    public interface Flyable
    {
        Observable<FlyTarget> FlyTarget { get; }
        Observable<FlyTarget> Location { get; }
        public void FlyTo(FlyTarget target);
    }
}
