using System;
using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class MapCreationStateChangedObjectActivator : MonoBehaviour, Injectable, Initializable, Cleanable
    {
        [SerializeField] 
        private GameObject _target;
        [SerializeField] 
        private MapCreationState _state;
        [SerializeField] 
        private bool _activate = true;
        
        private Observable<MapCreationState> _stateObservable;
        
        public void Inject(Resolver resolver)
        {
            _stateObservable = resolver.Resolve<Observable<MapCreationState>>();
        }

        private void Reset()
        {
            _target = gameObject;
        }

        public void Initialize()
        {
            UpdateShow();
            _stateObservable.OnValueChanged += OnStateChanged;
        }

        public void Clean()
        {
            _stateObservable.OnValueChanged -= OnStateChanged;
        }

        private void OnStateChanged(MapCreationState formervalue, MapCreationState newvalue)
        {
            UpdateShow();
        }

        private void UpdateShow()
        {
            _target.SetActive(_stateObservable.Value == _state && _activate ||
                              _stateObservable.Value != _state && !_activate);
        }
    }
}
