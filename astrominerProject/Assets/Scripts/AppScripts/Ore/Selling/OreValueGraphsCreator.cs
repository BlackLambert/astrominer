using System;
using System.Collections.Generic;
using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class OreValueGraphsCreator : MonoBehaviour, Injectable, Initializable, Cleanable
    {
        [SerializeField] private RectTransform _hook;

        private Pool<OreValueGraph, OresSettings.OreSettings, PrefabInstantiationArguments> _pool;
        private OresSettings _oresSettings;

        private List<OreValueGraph> _graphs = new List<OreValueGraph>();

        public void Inject(Resolver resolver)
        {
            _pool = resolver.Resolve<Pool<OreValueGraph, OresSettings.OreSettings, PrefabInstantiationArguments>>();
            _oresSettings = resolver.Resolve<OresSettings>();
        }

        public void Initialize()
        {
            CreateGraphs();
        }

        public void Clean()
        {
            ReturnGraphs();
        }

        private void CreateGraphs()
        {
            foreach (OreType oreType in Enum.GetValues(typeof(OreType)))
            {
                if (oreType == OreType.None)
                {
                    continue;
                }

                OreValueGraph graph = _pool.Request(_oresSettings.Get(oreType), 
                    PrefabInstantiationArguments.CreateFittedUIArgs(_hook));
                _graphs.Add(graph);
            }
        }

        private void ReturnGraphs()
        {
            foreach (OreValueGraph graph in _graphs)
            {
                _pool.Return(graph);
            }

            _graphs.Clear();
        }
    }
}