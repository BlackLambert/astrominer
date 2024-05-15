namespace SBaier.Astrominer
{
    public class MapCreationContext
    {
        public Observable<AsteroidAmountOption> SelectedAsteroidsAmountOption { get; } = new Observable<AsteroidAmountOption>();
        
        public bool IsValid => SelectedAsteroidsAmountOption.Value != null;
    }
}
