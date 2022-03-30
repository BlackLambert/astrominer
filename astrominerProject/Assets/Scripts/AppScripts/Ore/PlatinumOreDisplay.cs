using UnityEngine;

namespace SBaier.Astrominer
{
	public class PlatinumOreDisplay : OresValueDisplay
	{
		[SerializeField]
		private string _baseString = "Platinum: {0}";

		protected override string GetText()
		{
			return string.Format(_baseString, _item.Platinum.Amount.ToString("N0"));
		}
	}
}
