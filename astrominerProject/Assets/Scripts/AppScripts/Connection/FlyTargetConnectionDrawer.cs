using SBaier.DI;

namespace SBaier.Astrominer
{
    public class FlyTargetConnectionDrawer : FlightConnectionDrawer
    {
        private FlyableObject _flyable;
        private Player _player;

        public override void Inject(Resolver resolver)
        {
            base.Inject(resolver);
            _flyable = resolver.Resolve<FlyableObject>();
            _player = resolver.Resolve<Player>();
        }
        
        protected virtual void OnEnable()
        {
            UpdateConnections(_flyable.FlyTarget);
            _flyable.FlyTarget.OnValueChanged += OnActivePathChanged;
        }

        private void OnDisable()
        {
            _flyable.FlyTarget.OnValueChanged -= OnActivePathChanged;
        }

        private void OnActivePathChanged(FlightPath formervalue, FlightPath newvalue)
        {
            UpdateConnections(newvalue, _player.Color);
        }
    }
}
