namespace SBaier.Astrominer
{
    public class MapCreationContext
    {
        public Observable<AstroidAmountOption> SelectedAsteroidsAmountOption { get; } = new Observable<AstroidAmountOption>();
        public Observable<bool> Finished { get; } = new Observable<bool>();

        public bool IsValid => SelectedAsteroidsAmountOption.Value != null;
    }
}
