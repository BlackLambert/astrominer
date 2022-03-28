using UnityEngine;

namespace SBaier.Astrominer
{
	public class InventoryItemLevelDisplay : ItemPropertyDisplay<ExploitMachine>
	{
		[SerializeField]
		private string _baseText = "Lvl: {0}";

		protected override string GetText()
		{
			return string.Format(_baseText, _item.Level);
		}
	}
}
