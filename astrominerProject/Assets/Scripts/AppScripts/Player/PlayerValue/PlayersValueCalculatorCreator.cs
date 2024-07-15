using System;
using System.Collections.Generic;
using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class PlayersValueCalculatorCreator : MonoBehaviour, Injectable, Cleanable, Initializable
    {
        private Players _players;
        private Resolver _resolver;
        private Pool<PlayerValueCalculator, Player, PrefabInstantiationArguments> _pool;

        private List<PlayerValueCalculator> _calculators = new();

        public void Inject(Resolver resolver)
        {
            _players = resolver.Resolve<Players>();
            _pool = resolver.Resolve<Pool<PlayerValueCalculator, Player, PrefabInstantiationArguments>>();
        }

        public void Initialize()
        {
            CreateCalculators();
        }

        public void Clean()
        {
            foreach (PlayerValueCalculator calculator in _calculators)
            {
                _pool.Return(calculator);
            }
            _calculators.Clear();
        }

        private void CreateCalculators()
        {
            foreach (Player player in _players)
            {
                _calculators.Add(_pool.Request(player, new PrefabInstantiationArguments() { Parent = transform }));
            }
        }
    }
}