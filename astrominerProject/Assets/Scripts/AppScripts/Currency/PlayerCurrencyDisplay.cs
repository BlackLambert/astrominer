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

		protected override void OnEnable()
		{
			base.OnEnable();
			_item.Credits.OnAmountChanged += SetText;
		}

		private void OnDisable()
		{
			_item.Credits.OnAmountChanged -= SetText;
		}
	}
}
