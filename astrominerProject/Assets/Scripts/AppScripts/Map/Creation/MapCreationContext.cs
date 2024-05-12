namespace SBaier.Astrominer
{
    public class MapCreationContext
    {
        public Observable<AsteroidAmountOption> SelectedAsteroidsAmountOption { get; } = new Observable<AsteroidAmountOption>();
        public Observable<bool> Started { get; } = new Observable<bool>() { Value = true };
        public Observable<bool> Finished { get; } = new Observable<bool>() { Value = false };

        public bool IsValid => SelectedAsteroidsAmountOption.Value != null;
    }
}
