using UnityEngine;

namespace SBaier.Astrominer
{
    public abstract class OresValueDisplay : ItemPropertyDisplay<Ores>
    {
		protected override void Start()
		{
			base.Start();
            _item.OnValueChanged += SetText;
		}

		private void OnDestroy()
        {
            _item.OnValueChanged -= SetText;
        }
    }
}
