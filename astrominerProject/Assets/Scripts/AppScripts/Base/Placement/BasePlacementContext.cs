namespace SBaier.Astrominer
{
    public class BasePlacementContext
    {
        public Observable<bool> Started { get; } = false;
        public Observable<bool> PlacementIsValid { get; } = false;
        public Observable<bool> Placed { get; } = false;
    }
}
