using SBaier.DI;
using UnityEngine;
using UnityEngine.UI;

namespace SBaier.Astrominer
{
    public class GenerateMapButton : MonoBehaviour, Injectable
    {
        [SerializeField]
        private Button _button;

        private MapCreationContext _context;
        private MapGenerator _generator;

        public void Inject(Resolver resolver)
        {
            _context = resolver.Resolve<MapCreationContext>();
            _generator = resolver.Resolve<MapGenerator>();
        }

        private void OnEnable()
        {
            UpdateButtonInteractable();
            _context.OnAstroidsAmountOptionChanged += UpdateButtonInteractable;
            _button.onClick.AddListener(CreateMap);
        }

        private void OnDisable()
        {
            _context.OnAstroidsAmountOptionChanged -= UpdateButtonInteractable;
            _button.onClick.RemoveListener(CreateMap);
        }

        private void UpdateButtonInteractable()
        {
            _button.interactable = _context.IsValid;
        }

        private void CreateMap()
        {
            _generator.GenerateMap(_context.AstroidsAmountOption);
        }
    }
}
