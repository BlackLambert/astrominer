using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SBaier.Astrominer
{
    public class SceneCommandsExecuter : MonoBehaviour
    {
        [SerializeField]
        private List<SceneManagementCommand> _commands = new List<SceneManagementCommand>();

        public IEnumerator ExecuteCommands()
		{
            foreach (SceneManagementCommand command in _commands)
                yield return ExecuteCommand(command);
        }

        private IEnumerator ExecuteCommand(SceneManagementCommand command)
        {
            Debug.Log($"Execute {command.name}");
            switch (command)
            {
                case SceneLoadCommand loadCommand:
                    yield return SceneManager.LoadSceneAsync(loadCommand.SceneName, loadCommand.Mode);
                    break;
                case SceneUnloadCommand unloadCommand:
                    yield return SceneManager.UnloadSceneAsync(unloadCommand.SceneName);
                    break;
                default:
                    throw new NotImplementedException($"The {nameof(SceneManagementCommand)} of type {command.GetType()} " +
                        $"is not handled by {nameof(ExecuteCommand)}");
            }
        }
    }
}
