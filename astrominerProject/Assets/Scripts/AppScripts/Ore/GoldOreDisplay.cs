using UnityEngine;

namespace SBaier.Astrominer
{
    public class GoldOreDisplay : OresValueDisplay
	{
		[SerializeField]
		private string _baseString = "Gold: {0}";

		protected override string GetText()
		{
			return string.Format(_baseString, _item.Gold.Amount.ToString("N0"));
		}
	}
}
