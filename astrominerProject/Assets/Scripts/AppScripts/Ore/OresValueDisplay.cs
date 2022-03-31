using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class OresValueDisplay : ItemPropertyDisplay<Ores>
    {
        private OresSettings.OreSettings _oreSettings;
        [SerializeField]
        private OreType _oreType = OreType.None;

		public override void Inject(Resolver resolver)
		{
			base.Inject(resolver);
            _oreSettings = resolver.Resolve<OresSettings>().Get(_oreType);
        }

		protected override void Start()
		{
			base.Start();
            _item.OnValueChanged += SetText;
		}

		private void OnDestroy()
        {
            _item.OnValueChanged -= SetText;
        }

        protected override string GetText()
        {
            return string.Format(_oreSettings.DisplayString, _item[_oreType].Amount.ToString("N0"));
        }
    }
}
