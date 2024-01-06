using System;

namespace SBaier.Astrominer
{
    public interface Flyable
    {
        Observable<FlightPath> FlyTarget { get; }
        Observable<FlyTarget> Location { get; }
        public void FlyTo(FlightPath target);
    }
}
