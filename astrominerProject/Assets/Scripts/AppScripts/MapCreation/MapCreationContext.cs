using System;

namespace SBaier.Astrominer
{
    public class MapCreationContext
    {
        public event Action OnAstroidsAmountOptionChanged;

        public AstroidAmountOption AstroidsAmountOption
        {
            get => _astroidsAmountOption;
            set
            {
                _astroidsAmountOption = value;
                OnAstroidsAmountOptionChanged?.Invoke();
            }
        }

        public bool IsValid => _astroidsAmountOption != null;

        private AstroidAmountOption _astroidsAmountOption;
    }
}
