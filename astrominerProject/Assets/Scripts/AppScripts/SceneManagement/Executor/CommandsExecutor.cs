using System.Collections.Generic;
using UnityEngine;

namespace SBaier.Astrominer
{
    public interface CommandsExecutor
    {
        public bool ProcessRunning { get; }
        Observable<SceneChangeProcess> CurrentProcess { get; }
        void Execute(List<SceneChangeCommand> commands);
        void StopCurrentProcess();
    }
}
