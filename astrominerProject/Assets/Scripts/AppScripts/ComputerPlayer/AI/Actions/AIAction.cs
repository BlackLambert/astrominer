namespace SBaier.Astrominer
{
    public interface AIAction
    {
        public bool AllowsFollowAction { get; }
        public int GetCurrentWeight();
        public void Execute();
    }
}
