using System;
using System.Collections.Generic;
using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class AgentsCreator : MonoBehaviour, Injectable
    {
        [SerializeField] 
        private Transform _hook;

        private Pool<Agent, Agent.Arguments> _pool;
        private Factory<List<AIAction>, Player> _actionsFactory;
        private Players _players;

        private List<Agent> _agents = new List<Agent>();

        public void Inject(Resolver resolver)
        {
            _pool = resolver.Resolve<Pool<Agent, Agent.Arguments>>();
            _players = resolver.Resolve<Players>();
            _actionsFactory = resolver.Resolve<Factory<List<AIAction>, Player>>();
        }

        private void OnEnable()
        {
            CreateAgents();
        }

        private void CreateAgents()
        {
            foreach (Player player in _players)
            {
                if (!player.IsHuman)
                {
                    _agents.Add(CreateAgent(player));
                }
            }
        }

        private Agent CreateAgent(Player player)
        {
            Agent result = _pool.Request(CreateArguments(player));
            result.transform.SetParent(_hook, false);
            return result;
        }

        private Agent.Arguments CreateArguments(Player player)
        {
            return new Agent.Arguments()
            {
                Player = player,
                Actions = _actionsFactory.Create(player)
            };
        }
    }
}
