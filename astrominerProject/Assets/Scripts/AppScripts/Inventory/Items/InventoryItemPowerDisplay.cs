using UnityEngine;

namespace SBaier.Astrominer
{
	public class InventoryItemPowerDisplay : ItemPropertyDisplay<ExploitMachine>
	{
		[SerializeField]
		private string _baseText = "Power: {0}";

		protected override string GetText()
		{
			return string.Format(_baseText, _item.Power);
		}
	}
}
