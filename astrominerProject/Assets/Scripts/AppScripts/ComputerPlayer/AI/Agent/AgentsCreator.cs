using System.Collections.Generic;
using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class AgentsCreator : MonoBehaviour, Injectable, Initializable, Cleanable
    {
        [SerializeField] private Transform _hook;

        private Pool<Agent, Agent.Arguments, PrefabInstantiationArguments> _pool;
        private Ships _ships;
        private Factory<AgentActor, Ship> _agentActorFactory;

        private Dictionary<Ship, Agent> _agents = new();

        public void Inject(Resolver resolver)
        {
            _pool = resolver.Resolve<Pool<Agent, Agent.Arguments, PrefabInstantiationArguments>>();
            _ships = resolver.Resolve<Ships>();
            _agentActorFactory = resolver.Resolve<Factory<AgentActor, Ship>>();
        }

        public void Initialize()
        {
            CreateAgents();
            _ships.Values.OnItemAdded += TryCreateAgent;
            _ships.Values.OnItemRemoved += TryRemoveAgent;
        }

        public void Clean()
        {
            ClearAgents();
            _ships.Values.OnItemAdded -= TryCreateAgent;
            _ships.Values.OnItemRemoved -= TryRemoveAgent;
        }

        private void CreateAgents()
        {
            foreach (Ship ship in _ships.Values)
            {
                TryCreateAgent(ship);
            }
        }

        private void ClearAgents()
        {
            foreach (KeyValuePair<Ship,Agent> pair in _agents)
            {
                _pool.Return(pair.Value);
            }
            _agents.Clear();
        }

        private void TryCreateAgent(Ship ship)
        {
            if (!ship.Player.IsHuman)
            {
                _agents.Add(ship, CreateAgent(ship));
            }
        }

        private void TryRemoveAgent(Ship ship)
        {
            if (_agents.TryGetValue(ship, out Agent agent))
            {
                _pool.Return(agent);
                _agents.Remove(ship);
            }
        }

        private Agent CreateAgent(Ship ship)
        {
            Agent result = _pool.Request(
                CreateArguments(ship),
                new PrefabInstantiationArguments() { Parent = _hook });
            return result;
        }

        private Agent.Arguments CreateArguments(Ship ship)
        {
            return new Agent.Arguments()
            {
                Player = ship.Player,
                Actor = _agentActorFactory.Create(ship)
            };
        }
    }
}