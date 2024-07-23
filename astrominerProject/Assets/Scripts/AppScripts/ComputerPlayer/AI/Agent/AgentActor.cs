using System;
using System.Text;
using SBaier.AI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class AgentActor
    {
        private readonly AIBrain _brain;
        private readonly Node _actions;
        private readonly ReadonlyObservable<bool> _allowsFollowupAction;
        private readonly BasicLog _log;
        private readonly int _actionStackLimit;
        private readonly bool _loggingEnabled;
        private int _actionsAmount = 0;

        public AgentActor(AIBrain brain,
            Node actions,
            ReadonlyObservable<bool> allowsFollowupAction,
            BasicLog log,
            bool loggingEnabled,
            int actionStackLimit = 1000)
        {
            _brain = brain;
            _actions = actions;
            _allowsFollowupAction = allowsFollowupAction;
            _log = log;
            _loggingEnabled = loggingEnabled;
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
            _brain.Update();
            PrepareLog();
            _actions.Execute();

            if (_allowsFollowupAction.Value)
            {
                ExecuteNextActionsInternal(actionStackAmount + 1);
            }
        }

        private void PrepareLog()
        {
            if (!_loggingEnabled)
            {
                return;
            }

            string color = ColorUtility.ToHtmlStringRGB(_brain.Player.Color);
            _log.SetHeader($"{_actionsAmount}. Action of the Player <color=#{color}>{_brain.Player.Name}</color>");
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("Situation:");
            stringBuilder.Append($"\nCredits: {_brain.Credits}");
            stringBuilder.Append($"\nExploit Machines: {_brain.ExploitersInInventoryAmount}");
            _log.Add(new LogEntry(stringBuilder.ToString()));
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