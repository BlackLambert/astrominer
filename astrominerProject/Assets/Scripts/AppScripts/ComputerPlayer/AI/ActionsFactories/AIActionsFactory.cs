using SBaier.AI;

namespace SBaier.Astrominer
{
    public abstract class AIActionsFactory
    {
        protected Sequence CreateSequence(params Node[] nodes)
        {
            Sequence sequence = new Sequence();

            foreach (Node node in nodes)
            {
                sequence.AddChild(node);
            }

            return sequence;
        }
    }
}