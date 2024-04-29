using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class LoadingScreenDisplayer : MonoBehaviour, Injectable
    {
        private CommandsExecutor _commandsExecutor;
        private Pool<LoadingScreen, SceneChangeProcess> _loadingScreenPool;

        private LoadingScreen _currentLoadingScreen;

        public void Inject(Resolver resolver)
        {
            _commandsExecutor = resolver.Resolve<CommandsExecutor>();
            _loadingScreenPool = resolver.Resolve<Pool<LoadingScreen, SceneChangeProcess>>();
        }

        private void Start()
        {
            _commandsExecutor.CurrentProcess.OnValueChanged += OnLoadingProcessChanged;
            TryShowLoadingScreen(_commandsExecutor.CurrentProcess.Value);
        }

        private void OnDestroy()
        {
            _commandsExecutor.CurrentProcess.OnValueChanged -= OnLoadingProcessChanged;
            TryHideLoadingScreen();
        }

        private void OnLoadingProcessChanged(SceneChangeProcess formervalue, SceneChangeProcess newvalue)
        {
            TryHideLoadingScreen();
            TryShowLoadingScreen(newvalue);
        }

        private void TryHideLoadingScreen()
        {
            if (_currentLoadingScreen != null)
            {
                _commandsExecutor.CurrentProcess.Value.OnFinished -= TryHideLoadingScreen;
                _commandsExecutor.CurrentProcess.Value.OnStopped -= TryHideLoadingScreen;
                _loadingScreenPool.Return(_currentLoadingScreen);
                _currentLoadingScreen = null;
            }
        }

        private void TryShowLoadingScreen(SceneChangeProcess process)
        {
            if (_commandsExecutor.CurrentProcess.Value is { Finished: false })
            {
                _commandsExecutor.CurrentProcess.Value.OnFinished += TryHideLoadingScreen;
                _commandsExecutor.CurrentProcess.Value.OnStopped += TryHideLoadingScreen;
                _currentLoadingScreen = _loadingScreenPool.Request(process);
            }
        }
    }
}