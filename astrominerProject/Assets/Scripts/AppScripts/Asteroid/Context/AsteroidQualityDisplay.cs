using UnityEngine;

namespace SBaier.Astrominer
{
    public class AsteroidQualityDisplay : ItemPropertyDisplay<Asteroid>
	{
		[SerializeField]
		private string _baseString = "Quality: {0}";

		protected override string GetText()
		{
			return string.Format(_baseString, _item.Quality);
		}
	}
}
