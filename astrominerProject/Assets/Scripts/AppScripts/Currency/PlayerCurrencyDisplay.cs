using UnityEngine;

namespace SBaier.Astrominer
{
	public class PlayerCurrencyDisplay : ItemPropertyDisplay<Player>
	{
		[SerializeField]
		private string _baseString = "Credits: {0}";

		protected override string GetText()
		{
			return string.Format(_baseString, _item.Credits.ToString());
		}

		protected override void Start()
		{
			base.Start();
			_item.Credits.OnAmountChanged += SetText;
		}

		private void OnDestroy()
		{
			_item.Credits.OnAmountChanged -= SetText;
		}
	}
}
