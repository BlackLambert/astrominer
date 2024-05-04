using System.Threading.Tasks;
using SBaier.DI;

namespace SBaier.Astrominer
{
    public class LoadingScreenProcessStarter : ProcessStarterBase
    {
        private Pool<LoadingScreen, Process> _loadingScreenPool;
        private LoadingScreen _currentLoadingScreen;

        public override void Inject(Resolver resolver)
        {
            base.Inject(resolver);
            _loadingScreenPool = resolver.Resolve<Pool<LoadingScreen, Process>>();
        }

        protected override async Task StartProcess(Process process, bool immediately)
        {
            _currentLoadingScreen = _loadingScreenPool.Request(process);
            _currentLoadingScreen.transform.SetParent(null);
            await _currentLoadingScreen.Show(immediately);
            process.Start();
        }

        protected override async Task StopProcess(Process process)
        {
            process.Stop();
            await TryHideLoadingScreen(true);
        }

        protected override async Task CleanProcess(Process process, bool immediately)
        {
            await base.CleanProcess(process, immediately);
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