using SBaier.DI;

namespace SBaier.Astrominer
{
    public class AsteroidNameDisplay : ItemPropertyDisplay<Asteroid>, Injectable
    {
		protected override string GetText()
		{
			return _item.name;
		}
	}
}
