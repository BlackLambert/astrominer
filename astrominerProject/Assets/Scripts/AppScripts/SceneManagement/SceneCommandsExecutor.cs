using System;
using System.Collections;
using System.Collections.Generic;
using SBaier.DI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SBaier.Astrominer
{
    public class SceneCommandsExecutor : MonoBehaviour, Injectable
    {
        [SerializeField]
        private List<SceneManagementCommand> _commands = new List<SceneManagementCommand>();

        private MonoBehaviour _coroutineHelper;
        
        public void Inject(Resolver resolver)
        {
            _coroutineHelper = resolver.Resolve<MonoBehaviour>(BindingIds.CoroutineHelper);
        }
        
        protected void Execute()
        {
            _coroutineHelper.StartCoroutine(ExecuteCommands());
        }
        
        private IEnumerator ExecuteCommands()
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
