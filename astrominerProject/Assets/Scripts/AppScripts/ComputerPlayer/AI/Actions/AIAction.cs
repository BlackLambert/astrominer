namespace SBaier.Astrominer
{
    public interface AIAction
    {
        public bool AllowsFollowAction { get; }
        public float GetCurrentWeight(Ship ship);
        public void Execute(Ship ship);
    }
}
