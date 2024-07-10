using SBaier.AI;

namespace SBaier.Astrominer
{
    public class SendCarrierDroneAction : NodeBase
    {
        private readonly AIBrain _brain;
        private readonly Observable<bool> _allowsFollowupAction;

        public SendCarrierDroneAction(AIBrain brain, Observable<bool> allowsFollowupAction)
        {
            _brain = brain;
            _allowsFollowupAction = allowsFollowupAction;
        }
        
        public override bool Execute()
        {
            _brain.SendDroneToBestCollectOresTarget();
            _allowsFollowupAction.Value = true;
            return true;
        }
    }
}