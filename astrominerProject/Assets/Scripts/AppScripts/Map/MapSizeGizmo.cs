using System;
using System.Collections;
using System.Collections.Generic;
using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class MapSizeGizmo : MonoBehaviour, Injectable
    {
        [SerializeField] private Color _color = Color.cyan;
        
        private Map _map;
        
        public void Inject(Resolver resolver)
        {
            _map = resolver.Resolve<Map>();
        }

        private void OnDrawGizmos()
        {
            if (_map == null || _map.AsteroidAmountOption.Value == null)
            {
                return;
            }

            Gizmos.color = _color;
            Gizmos.DrawWireCube(_map.CenterPoint, _map.AsteroidAmountOption.Value.MapSize);
        }
    }
}
