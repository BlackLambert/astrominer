using System;
using System.Threading.Tasks;
using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public abstract class ProcessStarterBase : MonoBehaviour, Injectable
    {
        private ProcessQueue _queue;
        private Observable<Process> _currentProcess;

        public virtual async void Inject(Resolver resolver)
        {
            _queue = resolver.Resolve<ProcessQueue>();
            _currentProcess = resolver.Resolve<Observable<Process>>();

            _queue.OnEnqueue += OnProcessEnqueued;
            await TryStartNextProcess(false);
        }

        private async void OnDisable()
        {
            _queue.OnEnqueue -= OnProcessEnqueued;
            await TryStopCurrentProcess(true);
        }

        protected abstract Task StartProcess(Process process, bool immediately);
        protected abstract Task StopProcess(Process process);

        private async Task TryStartNextProcess(bool immediately)
        {
            if (_queue.HasNext && _currentProcess.Value == null)
            {
                Process process = _queue.Dequeue();
                _currentProcess.Value = process;
                process.OnStopped += OnProcessEnded;
                process.OnFinished += OnProcessEnded;
                await StartProcess(process, immediately);
            }
        }

        private async Task TryStopCurrentProcess(bool immediately)
        {
            Process process = _currentProcess.Value;
            if (process != null)
            {
                await CleanProcess(process, immediately);
                await StopProcess(process);
            }
        }

        protected virtual Task CleanProcess(Process process, bool immediately)
        {
            process.OnStopped -= OnProcessEnded;
            process.OnFinished -= OnProcessEnded;
            _currentProcess.Value = null;
            return Task.CompletedTask;
        }

        private async void OnProcessEnqueued()
        {
            await TryStartNextProcess(false);
        }

        private async void OnProcessEnded()
        {
            await CleanProcess(process: _currentProcess.Value, immediately: _queue.HasNext);
            await TryStartNextProcess(true);
        }
    }
}