namespace SBaier.Astrominer
{
    public class MapCreationContext
    {
        public Observable<AsteroidAmountOption> SelectedAsteroidsAmountOption { get; } = new Observable<AsteroidAmountOption>();
        public Observable<bool> Finished { get; } = new Observable<bool>();

        public bool IsValid => SelectedAsteroidsAmountOption.Value != null;
    }
}
