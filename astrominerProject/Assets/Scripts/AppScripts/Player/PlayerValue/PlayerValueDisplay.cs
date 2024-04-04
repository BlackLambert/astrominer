using System;

namespace SBaier.Astrominer
{
    public class PlayerValueDisplay : ItemPropertyDisplay<PlayerValue>
    {
        private string _format = "F1";
        
        protected override string GetText()
        {
            return _item.TotalValue.Value.ToString(_format);
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            _item.TotalValue.OnValueChanged += OnValueChanged;
        }
        
        private void OnDisable()
        {
            _item.TotalValue.OnValueChanged -= OnValueChanged;
        }

        private void OnValueChanged(float formervalue, float newvalue)
        {
            SetText();
        }

    }
}
