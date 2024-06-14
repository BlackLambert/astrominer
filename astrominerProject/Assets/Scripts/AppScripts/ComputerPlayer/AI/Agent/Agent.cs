using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class Agent : MonoBehaviour, Injectable, Initializable, Cleanable
    {
        private ActiveItem<Player> _activePlayer;
        private AgentActor _actor;
        private Player _player;

        public void Inject(Resolver resolver)
        {
            _activePlayer = resolver.Resolve<ActiveItem<Player>>();
            Arguments arguments = resolver.Resolve<Arguments>();
            _actor = arguments.Actor;
            _player = arguments.Player;
        }

        public void Initialize()
        {
            _activePlayer.OnValueChanged += OnActivePlayerChanged;
        }

        public void Clean()
        {
            _activePlayer.OnValueChanged -= OnActivePlayerChanged;
        }

        private void OnActivePlayerChanged(Player formervalue, Player newvalue)
        {
            PerformAction();
        }

        private void PerformAction()
        {
            if (_activePlayer.HasValue && _activePlayer.Value == _player)
            {
                _actor.ExecuteNextActions();
            }
        }

        public class Arguments
        {
            public AgentActor Actor;
            public Player Player;
        }
    }
}
