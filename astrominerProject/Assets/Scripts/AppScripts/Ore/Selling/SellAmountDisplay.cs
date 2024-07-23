using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class SellAmountDisplay : ItemPropertyDisplay<Ship, OresToSell>, Cleanable
    {
        [SerializeField] 
        private string _baseText = "{0} <color=red>(-{1})</color>";

        private OreType _oreType;

        public override void Inject(Resolver resolver)
        {
            base.Inject(resolver);
            _oreType = resolver.Resolve<OreType>();
        }

        public override void Initialize()
        {
            base.Initialize();
            _secondItem.Ores.OnValueChanged += SetText;
            _item.CollectedOres.OnValueChanged += SetText;
        }

        public void Clean()
        {
            _secondItem.Ores.OnValueChanged -= SetText;
            _item.CollectedOres.OnValueChanged -= SetText;
        }

        protected override string GetText()
        {
            return string.Format(_baseText, (int)_item.CollectedOres[_oreType].Amount,
                (int)_secondItem.Ores[_oreType].Amount);
        }
    }
}
