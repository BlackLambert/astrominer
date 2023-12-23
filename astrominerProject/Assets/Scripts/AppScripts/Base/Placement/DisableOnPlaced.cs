using System;
using System.Collections;
using System.Collections.Generic;
using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class DisableOnPlaced : MonoBehaviour, Injectable
    {
        [SerializeField] 
        private MonoBehaviour _behaviour;

        private BasePlacementContext _context;

        public void Inject(Resolver resolver)
        {
            _context = resolver.Resolve<BasePlacementContext>();
        }

        private void OnEnable()
        {
            UpdateEnabledState();
            _context.Placed.OnValueChanged += OnPlacedChanged;
        }

        private void OnDisable()
        {
            _context.Placed.OnValueChanged -= OnPlacedChanged;
        }

        private void OnPlacedChanged(bool formervalue, bool newvalue)
        {
            UpdateEnabledState();
        }

        private void UpdateEnabledState()
        {
            _behaviour.enabled = !_context.Placed.Value;
        }
    }
}
