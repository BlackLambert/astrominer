using System.Collections;

namespace SBaier.Astrominer
{
    public class SceneCommandsOnStartExecuter : SceneCommandsExecuter
    {
        private IEnumerator Start()
        {
            yield return ExecuteCommands();
        }
    }
}
