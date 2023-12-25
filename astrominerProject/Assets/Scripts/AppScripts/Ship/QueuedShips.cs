using System;
using System.Collections.Generic;

namespace SBaier.Astrominer
{
    public class QueuedShips
    {
        public event Action<Ship> OnEnqueued; 
        public event Action<Ship> OnDequeued; 

        private Queue<Ship> _queue = new Queue<Ship>();

        public bool HasNext()
        {
            return _queue.Count > 0;
        }

        public void Enqueue(Ship ship)
        {
            _queue.Enqueue(ship);
            OnEnqueued?.Invoke(ship);
        }

        public Ship Dequeue()
        {
            if (!HasNext())
            {
                throw new InvalidOperationException("There is no ship in the queue");
            }
            
            Ship ship = _queue.Dequeue();
            OnDequeued?.Invoke(ship);
            return ship;
        }
    }
}
