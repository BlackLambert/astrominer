using System.Collections.Generic;
using System.Linq;
using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class Agent : MonoBehaviour, Injectable
    {
        private ActiveItem<Ship> _activeShip;
        private Arguments _arguments;

        public void Inject(Resolver resolver)
        {
            _activeShip = resolver.Resolve<ActiveItem<Ship>>();
            _arguments = resolver.Resolve<Arguments>();
        }

        private void OnEnable()
        {
            _activeShip.OnValueChanged += OnActiveShipChanged;
        }

        private void OnDisable()
        {
            _activeShip.OnValueChanged -= OnActiveShipChanged;
        }

        private void OnActiveShipChanged(Ship formervalue, Ship newvalue)
        {
            ExecuteNextAction();
        }

        private void ExecuteNextAction()
        {
            if (!_activeShip.HasValue || _activeShip.Value.Player != _arguments.Player)
            {
                return;
            }

            AIAction action = _arguments.Actions.Aggregate((action1, action2) =>
                action1.GetCurrentWeight() > action2.GetCurrentWeight() ? action1 : action2);
            action.Execute();

            if (action.AllowsFollowAction)
            {
                ExecuteNextAction();
            }
        }

        public class Arguments
        {
            public Player Player;
            public List<AIAction> Actions;
        }
    }
}
