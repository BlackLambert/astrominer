using SBaier.DI;

namespace SBaier.Astrominer
{
    public class SelectedTargetConnectionDrawer : FlightConnectionDrawer
    {
        private ActiveItem<FlightPath> _activePath;

        public override void Inject(Resolver resolver)
        {
            base.Inject(resolver);
            _activePath = resolver.Resolve<ActiveItem<FlightPath>>();
        }
        
        protected virtual void OnEnable()
        {
            UpdateConnections(_activePath.Value);
            _activePath.OnValueChanged += OnActivePathChanged;
        }

        private void OnDisable()
        {
            _activePath.OnValueChanged -= OnActivePathChanged;
        }

        private void OnActivePathChanged(FlightPath formervalue, FlightPath newvalue)
        {
            UpdateConnections(newvalue);
        }
    }
}
