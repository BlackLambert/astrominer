using System.Collections.Generic;
using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class CommandsExecutionTriggerBehaviour : MonoBehaviour, Injectable
    {
        [SerializeField]
        private List<SceneChangeCommand> _commands = new List<SceneChangeCommand>();

        private CommandsExecutor _commandsExecutor;
        
        public virtual void Inject(Resolver resolver)
        {
            _commandsExecutor = resolver.Resolve<CommandsExecutor>();
        }

        protected void Execute()
        {
            _commandsExecutor.Execute(_commands);
        }
    }
}
