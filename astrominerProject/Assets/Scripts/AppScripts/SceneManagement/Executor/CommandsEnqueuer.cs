using System.Collections.Generic;
using UnityEngine;

namespace SBaier.Astrominer
{
    public interface CommandsEnqueuer
    {
        void Enqueue(List<SceneChangeCommand> commands);
    }
}
