using System;
using System.Collections.Generic;
using System.Linq;
using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class BasesCreator : MonoBehaviour, Injectable, Initializable, Cleanable
    {
        private Bases _bases;
        private Pool<Base, Player, PrefabInstantiationArguments> _pool;
        private BasePositions _positions;

        public void Inject(Resolver resolver)
        {
            _bases = resolver.Resolve<Bases>();
            _pool = resolver.Resolve<Pool<Base, Player, PrefabInstantiationArguments>>();
            _positions = resolver.Resolve<BasePositions>();
        }

        public void Initialize()
        {
            CreateBases();
        }

        public void Clean()
        {
            foreach (KeyValuePair<Player, Base> pair in _bases.Where(p => p.Value != null))
            {
                _pool.Return(pair.Value);
            }

            _bases.Clear();
        }

        private void CreateBases()
        {
            _bases.Clear();
            foreach (KeyValuePair<Player, Vector2> pair in _positions)
            {
                Base newBase = _pool.Request(pair.Key,
                    new PrefabInstantiationArguments() { Position = pair.Value, Parent = transform });
                _bases.Add(pair.Key, newBase);
            }
        }
    }
}