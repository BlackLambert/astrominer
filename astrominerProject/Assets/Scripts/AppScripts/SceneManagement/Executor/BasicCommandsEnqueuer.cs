using System.Collections.Generic;
using SBaier.DI;
using SBaier.Process;
using SBaier.Process.UI;

namespace SBaier.Astrominer
{
    public class BasicCommandsEnqueuer : CommandsEnqueuer, Injectable
    {
        private ProcessQueue _queue;

        public void Inject(Resolver resolver)
        {
            _queue = resolver.Resolve<ProcessQueue>();
        }
        
        public void Enqueue(List<SceneChangeCommand> commands)
        {
            List<Process.Process> processes = new List<Process.Process>();
            foreach (SceneChangeCommand command in commands)
            {
                processes.Add(new AsyncOperationProcess(() => command.Execute()));
            }
            SynchronousProcessGroup group = new SynchronousProcessGroup(processes);
            group.AddProperty(new ProcessName("Changing Scene..."));
            _queue.Enqueue(group);
        }
    }
}