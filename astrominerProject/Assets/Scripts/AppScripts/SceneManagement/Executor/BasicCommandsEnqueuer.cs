using System;
using System.Collections.Generic;
using SBaier.DI;

namespace SBaier.Astrominer
{
    public class BasicCommandsEnqueuer : CommandsEnqueuer, Injectable
    {
        private CoroutineHelper _coroutineHelper;
        private ProcessQueue _queue;

        public void Inject(Resolver resolver)
        {
            _coroutineHelper = resolver.Resolve<CoroutineHelper>();
            _queue = resolver.Resolve<ProcessQueue>();
        }
        
        public void Enqueue(List<SceneChangeCommand> commands)
        {
            SceneChangeProcess process = new SceneChangeProcess(commands, _coroutineHelper);
            _queue.Enqueue(process);
        }
    }
}