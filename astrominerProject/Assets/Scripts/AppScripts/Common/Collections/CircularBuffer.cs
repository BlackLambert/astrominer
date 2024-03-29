using System;
using System.Collections.Generic;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class CircularBuffer<T>
    {
        public int Count { get; private set; } = 0;
        
        private List<T> _buffer;
        private int _currentIndex = 0;
        private int _size;
        
        public CircularBuffer(int size)
        {
            _size = size;
            CreateBuffer(size);
        }

        private void CreateBuffer(int size)
        {
            _buffer = new List<T>(size);
            for (int i = 0; i < size; i++)
            {
                _buffer.Add(default);
            }
        }

        public void Push(T value)
        {
            _buffer[_currentIndex] = value;
            ChangeIndex(1);
            ChangeCount(1);
        }

        public T Pop()
        {
            if (Count == 0)
            {
                throw new ArgumentException("There is no element in the buffer");
            }

            T result = _buffer[_currentIndex];
            ChangeIndex(-1);
            ChangeCount(-1);
            return result;
        }

        public IEnumerable<T> GetLastXElementsReverse(int amount)
        {
            if (amount > Count)
            {
                throw new ArgumentException($"The requested amount of elements exceeds the count of {Count}");
            }
            
            if (amount < 0)
            {
                throw new ArgumentException($"The requested amount is less then zero");
            }
            
            if (amount > _size)
            {
                throw new ArgumentException($"The requested amount exceeds the buffer size of {_size}");
            }

            for (int i = 0; i < amount; i++)
            {
                int index = _currentIndex - 1 - i;
                index = index < 0 ? _size + index : index % _size;
                yield return _buffer[index];
            }
        }
            
        private void ChangeCount(int delta)
        {
            Count = Mathf.Clamp(Count + delta, 0, _size);
        }

        private void ChangeIndex(int delta)
        {
            int newIndex = _currentIndex + delta;
            _currentIndex = newIndex < 0 ? _size + delta : newIndex % _size;
        }
    }
}
