using System.Threading.Tasks;

namespace SBaier.Astrominer
{
    public class BasicProcessStarter: ProcessStarterBase
    {
        protected override Task StartProcess(Process process, bool immediately)
        {
            process.Start();
            return Task.CompletedTask;
        }

        protected override Task StopProcess(Process process)
        {
            process.Stop();
            return Task.CompletedTask;
        }
    }
}