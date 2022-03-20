using UnityEngine;

namespace SBaier.Astrominer
{
    public class AsteroidSizeDisplay : ItemPropertyDisplay<Asteroid>
	{
		[SerializeField]
		private string _baseString = "Size: {0}";

		protected override string GetText()
		{
			return string.Format(_baseString, _item.Size);
		}
	}
}
