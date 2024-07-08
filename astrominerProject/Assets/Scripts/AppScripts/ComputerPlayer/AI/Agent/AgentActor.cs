using System.Collections.Generic;
using SBaier.AI;

namespace SBaier.Astrominer
{
    public class AgentActor
    {
        private readonly AIBrain _brain;
        private readonly Node _actions;
        private readonly ReadonlyObservable<bool> _allowsFollowupAction;

        public AgentActor(
            AIBrain brain,
            Node actions,
            ReadonlyObservable<bool> allowsFollowupAction)
        {
            _brain = brain;
            _actions = actions;
            _allowsFollowupAction = allowsFollowupAction;
        }
        
        public void ExecuteNextActions()
        {
            _brain.Update();
            _actions.Execute();

            if (_allowsFollowupAction.Value)
            {
                ExecuteNextActions();
            }
        }
    }
}