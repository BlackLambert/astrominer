using System;
using SBaier.DI;
using UnityEngine;
using UnityEngine.UI;

namespace SBaier.Astrominer
{
    public class UpdateInteractableIfLocationIsFlyTarget : MonoBehaviour, Injectable, Initializable, Cleanable
    {
        [SerializeField] private Selectable _selectable;
        [SerializeField] private bool _interactable = true;

        private FlyableObject _flyable;
        private FlyTarget _targetLocation;

        public void Inject(Resolver resolver)
        {
            _flyable = resolver.Resolve<FlyableObject>();
            _targetLocation = resolver.Resolve<FlyTarget>();
        }

        private void Reset()
        {
            _selectable = GetComponent<Selectable>();
        }

        public void Initialize()
        {
            UpdateInteractable();
            _flyable.Location.OnValueChanged += OnLocationChanged;
        }

        public void Clean()
        {
            _flyable.Location.OnValueChanged -= OnLocationChanged;
        }

        private void OnLocationChanged(FlyTarget formervalue, FlyTarget newvalue)
        {
            UpdateInteractable();
        }

        private void UpdateInteractable()
        {
            bool isLocation = Equals(_flyable.Location.Value, _targetLocation);
            _selectable.interactable = !(isLocation^_interactable);
        }
    }
}
