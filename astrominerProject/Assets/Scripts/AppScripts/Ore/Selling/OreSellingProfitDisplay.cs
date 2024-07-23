using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class OreSellingProfitDisplay : ItemPropertyDisplay<OresToSell>, Cleanable
    {
        [SerializeField] 
        private string _format = "F0";
        
        private OreBank _oreBank;
        
        public override void Inject(Resolver resolver)
        {
            base.Inject(resolver);
            _oreBank = resolver.Resolve<OreBank>();
        }

        public override void Initialize()
        {
            base.Initialize();
            _item.Ores.OnValueChanged += SetText;
        }

        public void Clean()
        {
            _item.Ores.OnValueChanged -= SetText;
        }

        protected override string GetText()
        {
            return $"+{_oreBank.CalculateCreditsFor(_item.Ores).ToString(_format)}";
        }
    }
}