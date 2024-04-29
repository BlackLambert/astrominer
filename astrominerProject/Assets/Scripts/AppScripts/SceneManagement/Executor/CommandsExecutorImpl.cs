using System;
using System.Collections.Generic;
using SBaier.DI;

namespace SBaier.Astrominer
{
    public class CommandsExecutorImpl : CommandsExecutor, Injectable
    {
        public bool ProcessRunning => CurrentProcess.Value is { Finished: false };
        public Observable<SceneChangeProcess> CurrentProcess { get; } = new ();

        private CoroutineHelper _coroutineHelper;

        public void Inject(Resolver resolver)
        {
            _coroutineHelper = resolver.Resolve<CoroutineHelper>();
        }
        
        public void Execute(List<SceneChangeCommand> commands)
        {
            ValidateExecute();
            StartExecution(commands);
        }

        public void StopCurrentProcess()
        {
            ValidateStopProcess();
            CurrentProcess.Value.Stop();
            CurrentProcess.Value = null;
        }

        private void StartExecution(List<SceneChangeCommand> commands)
        {
            SceneChangeProcess process = new SceneChangeProcess(commands, _coroutineHelper);
            process.Execute();
            CurrentProcess.Value = process;
        }

        private void ValidateExecute()
        {
            if (CurrentProcess.Value is { Finished: false })
            {
                throw new InvalidOperationException("Failed to execute scene change commands. " +
                                                    "There is already a scene change process ongoing.");
            }
        }

        private void ValidateStopProcess()
        {
            if (CurrentProcess.Value is { Finished: true })
            {
                throw new InvalidOperationException("Failed to stop scene change commands execution. " +
                                                    "There is no scene change process in progress.");
            }
        }
    }
}