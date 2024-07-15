using System;
using SBaier.AI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class AgentActor
    {
        private readonly AIBrain _brain;
        private readonly Node _actions;
        private readonly ReadonlyObservable<bool> _allowsFollowupAction;
        private readonly int _actionStackLimit;
        private int _actionsAmount = 0;

        public AgentActor(
            AIBrain brain,
            Node actions,
            ReadonlyObservable<bool> allowsFollowupAction,
            int actionStackLimit = 1000)
        {
            _brain = brain;
            _actions = actions;
            _allowsFollowupAction = allowsFollowupAction;
            _actionStackLimit = actionStackLimit;
        }
        
        public void ExecuteNextActions()
        {
            ExecuteNextActionsInternal(0);
        }

        private void ExecuteNextActionsInternal(int actionStackAmount)
        {
            ValidateActionStackAmount(actionStackAmount);
            _actionsAmount++;
            Debug.Log($"_______ ACTION {_actionsAmount} _______");
            _brain.Update();
            _actions.Execute();

            if (_allowsFollowupAction.Value)
            {
                ExecuteNextActionsInternal(actionStackAmount + 1);
            }
        }

        private void ValidateActionStackAmount(int actionStackAmount)
        {
            if (actionStackAmount > _actionStackLimit)
            {
                throw new InvalidOperationException("Failed to execute the next AI action. " +
                                                    $"The action stack limit {_actionStackLimit} was reached");
            }
        }
    }
}