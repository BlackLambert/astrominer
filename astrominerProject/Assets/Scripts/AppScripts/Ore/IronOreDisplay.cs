using UnityEngine;

namespace SBaier.Astrominer
{
	public class IronOreDisplay : OresValueDisplay
	{
		[SerializeField]
		private string _baseString = "Iron: {0}";

		protected override string GetText()
		{
			return string.Format(_baseString, _item.Iron.Amount.ToString("N0"));
		}
	}
}
