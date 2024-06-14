using SBaier.DI;

namespace SBaier.Astrominer
{
    public class AgentInstaller : MonoInstaller, Injectable
    {
        private Agent.Arguments _arguments;

        public void Inject(Resolver resolver)
        {
            _arguments = resolver.Resolve<Agent.Arguments>();
        }
        
        public override void InstallBindings(Binder binder)
        {
            binder.BindInstance(_arguments.Player);
            binder.BindInstance(_arguments);
        }
    }
}
