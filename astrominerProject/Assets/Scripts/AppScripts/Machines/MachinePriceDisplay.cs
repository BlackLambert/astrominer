using UnityEngine;

namespace SBaier.Astrominer
{
	public class MachinePriceDisplay : ItemPropertyDisplay<ExploitMachineLevelSettings>
	{
		[SerializeField]
		private string _baseText = "Price: {0}";

		protected override string GetText()
		{
			string price = _item.Price.ToString("N0");
			return string.Format(_baseText, price);
		}
	}
}
