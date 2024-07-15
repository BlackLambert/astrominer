using System;

namespace SBaier.Astrominer
{
    public class PlayerValue
    {
        public event Action OnReset;
        public Observable<float> TotalValue { get; } = 0;
        public CircularBuffer<float> ValueHistory { get; private set; }
        
        public PlayerValue(int valueHistoryBufferSize = 100)
        {
            ValueHistory = new CircularBuffer<float>(valueHistoryBufferSize);
        }

        public void Reset()
        {
            TotalValue.Value = 0;
            ValueHistory.Clear();
            OnReset?.Invoke();
        }
    }
}
