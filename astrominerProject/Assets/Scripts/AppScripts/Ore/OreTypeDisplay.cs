using SBaier.DI;

namespace SBaier.Astrominer
{
    public class OreTypeDisplay : ItemPropertyDisplay<OreType>, Injectable, Initializable
    {
        protected override string GetText()
        {
            return _item.ToString();
        }
    }
}
