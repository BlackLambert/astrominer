using SBaier.DI;

namespace SBaier.Astrominer
{
    public class ShowSellOresPopupButton : ShowPopupButton<SellOresPopupContent>
    {
        private Ship _ship;
        
        public override void Inject(Resolver resolver)
        {
            base.Inject(resolver);
            _ship = resolver.Resolve<Ship>();
        }

        public override void Initialize()
        {
            base.Initialize();
            _button.interactable = _ship.IsAtBase;
        }
    }
}
