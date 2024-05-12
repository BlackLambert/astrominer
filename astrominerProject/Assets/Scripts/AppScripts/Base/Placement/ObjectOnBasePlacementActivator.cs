using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class ObjectOnBasePlacementActivator : MonoBehaviour, Injectable, Initializable, Cleanable
    {
        [SerializeField] 
        private GameObject _target;
        
        private BasesPlacementContext _context;
        
        public void Inject(Resolver resolver)
        {
            _context = resolver.Resolve<BasesPlacementContext>();
        }

        public void Initialize()
        {
            UpdateShow();
            _context.Started.OnValueChanged += OnStartedChanged;
            _context.Finished.OnValueChanged += OnFinishedChanged;
        }

        public void Clean()
        {
            _context.Started.OnValueChanged -= OnStartedChanged;
            _context.Finished.OnValueChanged -= OnFinishedChanged;
        }

        private void OnStartedChanged(bool formervalue, bool newvalue)
        {
            UpdateShow();
        }

        private void OnFinishedChanged(bool formervalue, bool newvalue)
        {
            UpdateShow();
        }

        private void UpdateShow()
        {
            _target.SetActive(_context.Started.Value && !_context.Finished.Value);
        }
    }
}
