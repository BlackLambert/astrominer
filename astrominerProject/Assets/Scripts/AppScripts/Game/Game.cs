namespace SBaier.Astrominer
{
    public class Game
    {
        public Observable<float> ExploitedPercentage { get; } = 0;
        public Observable<bool> Finished { get; } = false;
        public Observable<Player> PLayerWon { get; } = new Observable<Player>();
    }
}
