using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class AstroidsAmountContextUpdater : MonoBehaviour, Injectable
    {
        private MapCreationContext _context;
        private ActiveItem<AstroidAmountOption> _selectedAstroidsAmount;

        public void Inject(Resolver resolver)
        {
            _context = resolver.Resolve<MapCreationContext>();
            _selectedAstroidsAmount = resolver.Resolve<ActiveItem<AstroidAmountOption>>();
        }

        private void OnEnable()
        {
            AddListeners();
        }

        private void OnDisable()
        {
            RemoveListeners();
        }

        private void AddListeners()
        {
            _selectedAstroidsAmount.OnValueChanged += OnSelectedAstroidsAmountChanged;
        }

        private void RemoveListeners()
        {
            _selectedAstroidsAmount.OnValueChanged -= OnSelectedAstroidsAmountChanged;
        }

        private void OnSelectedAstroidsAmountChanged()
        {
            _context.AstroidsAmountOption = _selectedAstroidsAmount.Value;
            Debug.Log($"Astroid amout changed to: {_selectedAstroidsAmount.Value}");
        }
    }
}
