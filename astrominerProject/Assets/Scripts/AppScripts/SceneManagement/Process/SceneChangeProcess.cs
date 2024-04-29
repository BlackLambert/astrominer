using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SBaier.Astrominer
{
    public class SceneChangeProcess
    {
        public event Action OnFinished;
        public event Action OnStopped;
        public float Progress => _commandsAmount > 0 ? (float)_index / _commandsAmount : 1;
        public bool Finished { get; private set; } = false;

        private List<SceneChangeCommand> _commands;
        private CoroutineHelper _coroutineHelper;
        private Coroutine _routine;
        private int _index = 0;
        private int _commandsAmount;

        public SceneChangeProcess(
            List<SceneChangeCommand> commands,
            CoroutineHelper coroutineHelper)
        {
            _commands = commands;
            _coroutineHelper = coroutineHelper;
            _commandsAmount = commands.Count;
        }

        public void Execute()
        {
            ValidateExecute();
            _routine = _coroutineHelper.StartCoroutine(ExecuteCommands());
        }

        public void Stop()
        {
            ValidateStop();
            _coroutineHelper.StopCoroutine(_routine);
            _routine = null;
            OnStopped?.Invoke();
        }
        
        private IEnumerator ExecuteCommands()
        {
            foreach (SceneChangeCommand command in _commands)
                yield return ExecuteCommand(command);
            Finish();
        }

        private IEnumerator ExecuteCommand(SceneChangeCommand command)
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
                    throw new NotImplementedException($"The {nameof(SceneChangeCommand)} of type {command.GetType()} " +
                                                      $"is not handled by {nameof(ExecuteCommand)}");
            }

            _index++;
        }

        private void ValidateExecute()
        {
            if (_routine != null)
            {
                throw new InvalidOperationException("Execute called on a process that is already running");
            }
        }

        private void ValidateStop()
        {
            if (_routine == null)
            {
                throw new InvalidOperationException("Stop called on a process that is not running");
            }
        }

        private void Finish()
        {
            _routine = null;
            Finished = true;
            OnFinished?.Invoke();
        }
    }
}