using System;
using SBaier.DI;

namespace SBaier.Astrominer
{
    public class PlayerValueDisplay : ItemPropertyDisplay<PlayerValue>, Cleanable
    {
        private string _format = "F1";
        
        protected override string GetText()
        {
            return _item.TotalValue.Value.ToString(_format);
        }

        public override void Initialize()
        {
            base.Initialize();
            _item.TotalValue.OnValueChanged += OnValueChanged;
        }
        
        public void Clean()
        {
            _item.TotalValue.OnValueChanged -= OnValueChanged;
        }

        private void OnValueChanged(float formervalue, float newvalue)
        {
            SetText();
        }

    }
}
