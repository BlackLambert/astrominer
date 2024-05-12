using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class OresValueDisplay : ItemPropertyDisplay<Ores>, Cleanable
    {
        private OresSettings.OreSettings _oreSettings;
        [SerializeField]
        private OreType _oreType = OreType.None;

		public override void Inject(Resolver resolver)
		{
			base.Inject(resolver);
            _oreSettings = resolver.Resolve<OresSettings>().Get(_oreType);
        }

        public override void Initialize()
		{
			base.Initialize();
            _item.OnValueChanged += SetText;
		}

        public void Clean()
        {
            _item.OnValueChanged -= SetText;
        }

        protected override string GetText()
        {
            return string.Format(_oreSettings.DisplayString, _item[_oreType].Amount.ToString("N0"));
        }
    }
}
