using SBaier.DI;

namespace SBaier.Astrominer
{
    public class SelectedTargetConnectionDrawer : FlightConnectionDrawer, Initializable, Cleanable
    {
        private ActiveItem<FlightPath> _activePath;

        public override void Inject(Resolver resolver)
        {
            base.Inject(resolver);
            _activePath = resolver.Resolve<ActiveItem<FlightPath>>();
        }
        
        public void Initialize()
        {
            UpdateConnections(_activePath.Value);
            _activePath.OnValueChanged += OnActivePathChanged;
        }

        public override void Clean()
        {
            base.Clean();
            _activePath.OnValueChanged -= OnActivePathChanged;
        }

        private void OnActivePathChanged(FlightPath formervalue, FlightPath newvalue)
        {
            UpdateConnections(newvalue);
        }
    }
}
