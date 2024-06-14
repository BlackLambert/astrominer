using System.Collections;
using System.Collections.Generic;
using SBaier.AI;

namespace SBaier.Astrominer
{
    public class AgentActor
    {
        private readonly Node _baseNode;
        private readonly IEnumerable _weighters;
        private readonly ReadonlyObservable<bool> _allowsFollowupAction;

        public AgentActor(
            Node baseNode,
            IEnumerable<Weighter> weighters,
            ReadonlyObservable<bool> allowsFollowupAction)
        {
            _baseNode = baseNode;
            _weighters = weighters;
            _allowsFollowupAction = allowsFollowupAction;
        }
        
        public void ExecuteNextActions()
        {
            UpdateWeight();
            _baseNode.Execute();

            if (_allowsFollowupAction.Value)
            {
                ExecuteNextActions();
            }
        }
        
        private void UpdateWeight()
        {
            foreach (Weighter weighter in _weighters)
            {
                weighter.UpdateWeight();
            }
        }
    }
}