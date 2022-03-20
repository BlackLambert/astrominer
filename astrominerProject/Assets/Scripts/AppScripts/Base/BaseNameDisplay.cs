using SBaier.DI;

namespace SBaier.Astrominer
{
    public class BaseNameDisplay : ItemPropertyDisplay<Base>, Injectable
	{
		protected override string GetText()
		{
			return _item.name;
		}
	}
}
