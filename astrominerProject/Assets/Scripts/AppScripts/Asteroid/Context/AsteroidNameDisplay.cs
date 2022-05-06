using SBaier.DI;

namespace SBaier.Astrominer
{
    public class AsteroidNameDisplay : ItemPropertyDisplay<Asteroid>
    {
		protected override string GetText()
		{
			return _item.name;
		}
	}
}
