using SBaier.DI;

namespace SBaier.Astrominer
{
    public class FlyTargetConnectionDrawer : FlightConnectionDrawer, Initializable
    {
        private FlyableObject _flyable;
        private Player _player;

        public override void Inject(Resolver resolver)
        {
            base.Inject(resolver);
            _flyable = resolver.Resolve<FlyableObject>();
            _player = resolver.Resolve<Player>();
        }
        
        public void Initialize()
        {
            UpdateConnections(_flyable.FlyTarget);
            _flyable.FlyTarget.OnValueChanged += OnActivePathChanged;
        }

        public override void Clean()
        {
            base.Clean();
            _flyable.FlyTarget.OnValueChanged -= OnActivePathChanged;
        }

        private void OnActivePathChanged(FlightPath formervalue, FlightPath newvalue)
        {
            UpdateConnections(newvalue, _player.Color);
        }
    }
}
