using SBaier.DI;

namespace SBaier.Astrominer
{
    public class CommandsOnInitTrigger : CommandsExecutionTriggerBehaviour, Initializable
    {
        public void Initialize()
        {
            Execute();
        }
    }
}
