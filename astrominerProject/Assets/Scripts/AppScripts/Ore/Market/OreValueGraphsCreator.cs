using System;
using System.Collections.Generic;
using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class OreValueGraphsCreator : MonoBehaviour, Injectable
    {
        [SerializeField] 
        private RectTransform _hook;
        
        private Pool<OreValueGraph, OresSettings.OreSettings> _pool;
        private OresSettings _oresSettings;

        private List<OreValueGraph> _graphs = new List<OreValueGraph>();
        
        public void Inject(Resolver resolver)
        {
            _pool = resolver.Resolve<Pool<OreValueGraph, OresSettings.OreSettings>>();
            _oresSettings = resolver.Resolve<OresSettings>();
        }

        private void Start()
        {
            CreateGraphs();
        }

        private void OnDestroy()
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

                OreValueGraph graph = _pool.Request(_oresSettings.Get(oreType));
                graph.transform.SetParent(_hook, false);
                graph.transform.localScale = Vector3.one;
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
