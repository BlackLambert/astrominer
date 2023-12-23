using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class BasePlacementValidColorizer : MonoBehaviour, Injectable
    {
        [SerializeField] 
        private SpriteRenderer _renderer;

        [SerializeField] 
        private Color _validColor;

        [SerializeField] 
        private Color _invalidColor;

        [SerializeField] 
        private Color _placedColor;

        private BasePlacementContext _context;

        public void Inject(Resolver resolver)
        {
            _context = resolver.Resolve<BasePlacementContext>();
        }

        private void OnEnable()
        {
            Colorize();
            _context.PlacementIsValid.OnValueChanged += OnIsValidChanged;
            _context.Placed.OnValueChanged += OnPlacedChanged;
        }

        private void OnDisable()
        {
            _context.PlacementIsValid.OnValueChanged -= OnIsValidChanged;
            _context.Placed.OnValueChanged -= OnPlacedChanged;
        }

        private void OnIsValidChanged(bool formervalue, bool newvalue)
        {
            Colorize();
        }

        private void OnPlacedChanged(bool formervalue, bool newvalue)
        {
            Colorize();
        }

        private void Colorize()
        {
            _renderer.color = GetColor();
        }

        private Color GetColor()
        {
            if (_context.Placed.Value)
            {
                return _placedColor;
            }
            if(_context.PlacementIsValid.Value)
            {
                return _validColor;
            }
            return _invalidColor;
        }
    }
}
