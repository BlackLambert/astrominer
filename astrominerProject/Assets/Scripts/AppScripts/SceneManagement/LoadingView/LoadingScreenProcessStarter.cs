using System.Threading;
using System.Threading.Tasks;
using SBaier.DI;
using SBaier.Process;

namespace SBaier.Astrominer
{
    public class LoadingScreenProcessStarter : ProcessStarterBase
    {
        private Pool<LoadingScreen, Process.Process> _loadingScreenPool;
        private LoadingScreen _currentLoadingScreen;

        public override void Inject(Resolver resolver)
        {
            base.Inject(resolver);
            _loadingScreenPool = resolver.Resolve<Pool<LoadingScreen, Process.Process>>();
        }

        protected override async Task RunProcess(Process.Process process, CancellationToken token, bool immediately)
        {
            _currentLoadingScreen = _loadingScreenPool.Request(process);
            _currentLoadingScreen.transform.SetParent(null);
            await _currentLoadingScreen.Show(immediately);
            await process.Run(token);
        }

        protected override async Task CleanOnProcessEnded(Process.Process process, bool immediately)
        {
            await TryHideLoadingScreen(immediately);
        }

        private async Task TryHideLoadingScreen(bool immediately)
        {
            if (_currentLoadingScreen != null)
            {
                await _currentLoadingScreen.Hide(immediately);
                _loadingScreenPool.Return(_currentLoadingScreen);
                _currentLoadingScreen = null;
            }
        }
    }
}