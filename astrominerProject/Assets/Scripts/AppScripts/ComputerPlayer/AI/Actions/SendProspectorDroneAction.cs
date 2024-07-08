using SBaier.AI;

namespace SBaier.Astrominer
{
    public class SendProspectorDroneAction : NodeBase
    {
        private readonly AIBrain _brain;
        private readonly Observable<bool> _allowsFollowupAction;
        
        public SendProspectorDroneAction(
            AIBrain brain,
            Observable<bool> allowsFollowupAction)
        {
            _brain = brain;
            _allowsFollowupAction = allowsFollowupAction;
        }

        public override bool Execute()
        {
            _brain.SendDroneToBestProspectTarget();
            _allowsFollowupAction.Value = true;
            return true;
        }
    }
}