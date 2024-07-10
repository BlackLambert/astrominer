using SBaier.AI;

namespace SBaier.Astrominer
{
    public class SellOresAction : NodeBase
    {
        private readonly AIBrain _brain;
        private readonly Observable<bool> _allowsFollowupAction;

        public SellOresAction(AIBrain brain, Observable<bool> allowsFollowupAction)
        {
            _brain = brain;
            _allowsFollowupAction = allowsFollowupAction;
        }
        
        public override bool Execute()
        {
            _brain.SellOres();
            _allowsFollowupAction.Value = true;
            return true;
        }
    }
}