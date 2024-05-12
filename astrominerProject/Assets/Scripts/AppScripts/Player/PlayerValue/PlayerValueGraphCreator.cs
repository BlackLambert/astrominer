using System;
using System.Collections.Generic;
using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class PlayerValueGraphCreator : MonoBehaviour, Injectable, Initializable
    {
        [SerializeField] 
        private RectTransform _hook;
        
        private Players _players;
        private Pool<PlayerValueGraph, Player, PrefabInstantiationArguments> _pool;
        private List<PlayerValueGraph> _graphs = new List<PlayerValueGraph>();
        
        public void Inject(Resolver resolver)
        {
            _players = resolver.Resolve<Players>();
            _pool = resolver.Resolve<Pool<PlayerValueGraph, Player, PrefabInstantiationArguments>>();
        }

        public void Initialize()
        {
            CreateGraphs();
        }

        public void Clean()
        {
            foreach (PlayerValueGraph graph in _graphs)
            {
                _pool.Return(graph);
            }
            _graphs.Clear();
        }
        
        private void CreateGraphs()
        {
            foreach (Player player in _players)
            {
                _graphs.Add(_pool.Request(player, PrefabInstantiationArguments.CreateFittedUIArgs(_hook)));
            }
        }
    }
}
