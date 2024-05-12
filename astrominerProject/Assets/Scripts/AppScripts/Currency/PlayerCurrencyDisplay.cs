using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
	public class PlayerCurrencyDisplay : ItemPropertyDisplay<Player>, Cleanable
	{
		[SerializeField]
		private string _baseString = "Credits: {0}";
		

		protected override string GetText()
		{
			return string.Format(_baseString, _item.Credits.ToString());
		}

		public override void Initialize()
		{
			base.Initialize();
			_item.Credits.OnAmountChanged += SetText;
		}

		public void Clean()
		{
			_item.Credits.OnAmountChanged -= SetText;
		}
	}
}
