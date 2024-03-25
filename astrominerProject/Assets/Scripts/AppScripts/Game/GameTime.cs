namespace SBaier.Astrominer
{
    public class GameTime
    {
        public Observable<float> Value { get; } = 0;
        public Observable<bool> Paused { get; } = true;
    }
}
