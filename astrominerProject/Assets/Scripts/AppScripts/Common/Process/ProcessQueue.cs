using System;
using System.Collections.Generic;

namespace SBaier.Astrominer
{
    public class ProcessQueue
    {
        public event Action OnEnqueue; 
        public bool HasNext => _queue.Count > 0;
        private Queue<Process> _queue = new Queue<Process>();
        
        public void Enqueue(Process process)
        {
            _queue.Enqueue(process);
            OnEnqueue?.Invoke();
        }

        public Process Dequeue()
        {
            return _queue.Dequeue();
        }

        public bool TryDequeue(out Process process)
        {
            return _queue.TryDequeue(out process);
        }
    }
}