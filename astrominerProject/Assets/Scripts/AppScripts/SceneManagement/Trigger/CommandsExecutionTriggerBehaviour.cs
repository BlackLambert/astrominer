using System.Collections.Generic;
using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class CommandsExecutionTriggerBehaviour : MonoBehaviour, Injectable
    {
        [SerializeField]
        private List<SceneChangeCommand> _commands = new List<SceneChangeCommand>();

        private CommandsEnqueuer _commandsEnqueuer;
        
        public virtual void Inject(Resolver resolver)
        {
            _commandsEnqueuer = resolver.Resolve<CommandsEnqueuer>();
        }

        protected void Execute()
        {
            _commandsEnqueuer.Enqueue(_commands);
        }
    }
}
