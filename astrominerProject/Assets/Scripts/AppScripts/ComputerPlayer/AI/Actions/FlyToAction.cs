using System;
using SBaier.AI;

namespace SBaier.Astrominer
{
    public class FlyToAction : NodeBase
    {
        private readonly AIBrain _brain;
        private readonly Observable<bool> _allowsFollowupAction;
        private readonly Func<FlyTarget> _getFlyTargetFunction;

        public FlyToAction(AIBrain brain, 
            Observable<bool> allowsFollowupAction,
            Func<FlyTarget> getFlyTargetFunction)
        {
            _brain = brain;
            _allowsFollowupAction = allowsFollowupAction;
            _getFlyTargetFunction = getFlyTargetFunction;
        }

        public override bool Execute()
        {
            _brain.FlyTo(_getFlyTargetFunction());
            _allowsFollowupAction.Value = false;
            return true;
        }
    }
}