using SBaier.DI;

namespace SBaier.Astrominer
{
    public class AddHumanPlayerButton : AddPlayerButton
    {
        protected override bool isHuman => true;
    }
}
