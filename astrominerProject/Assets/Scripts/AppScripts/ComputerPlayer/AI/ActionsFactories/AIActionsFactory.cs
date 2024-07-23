using SBaier.AI;
using SBaier.DI;

namespace SBaier.Astrominer
{
    public abstract class AIActionsFactory : Injectable
    {
        protected AISettings _generalSettings;
        
        public virtual void Inject(Resolver resolver)
        {
            _generalSettings = resolver.Resolve<AISettings>();
        }
        
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