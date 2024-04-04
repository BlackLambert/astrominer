using System;
using System.Collections.Generic;
using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class PlayerValueGraphCreator : MonoBehaviour, Injectable
    {
        [SerializeField] 
        private RectTransform _hook;
        
        private Players _players;
        private Pool<PlayerValueGraph, Player> _pool;
        private List<PlayerValueGraph> _graphs = new List<PlayerValueGraph>();
        
        public void Inject(Resolver resolver)
        {
            _players = resolver.Resolve<Players>();
            _pool = resolver.Resolve<Pool<PlayerValueGraph, Player>>();
        }

        private void OnEnable()
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
                PlayerValueGraph graph = _pool.Request(player);
                graph.transform.SetParent(_hook, false);
                graph.transform.localScale = Vector3.one;
                _graphs.Add(graph);
            }
        }
    }
}
