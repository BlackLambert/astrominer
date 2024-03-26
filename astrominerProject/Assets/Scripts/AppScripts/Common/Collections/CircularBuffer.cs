using System;
using System.Collections.Generic;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class CircularBuffer<T>
    {
        private List<T> _buffer;
        private int _count = 0;
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
            if (_count == 0)
            {
                throw new ArgumentException("There is no element in the buffer");
            }

            T result = _buffer[_currentIndex];
            ChangeIndex(-1);
            ChangeCount(-1);
            return result;
        }

        public IEnumerable<T> GetLastXElements(int amount)
        {
            if (_count > amount || _count < amount)
            {
                throw new ArgumentException($"The requested amount of elements exceeds the count of {_count}");
            }

            int currentIndex = _currentIndex;
            for (int i = 0; i < amount; i++)
            {
                yield return _buffer[currentIndex];
                int newIndex = _currentIndex - 1;
                currentIndex = newIndex < 0 ? _size - 1 : newIndex % _size;
            }
        }
            
        private void ChangeCount(int delta)
        {
            _count = Mathf.Clamp(_count + delta, 0, _size);
        }

        private void ChangeIndex(int delta)
        {
            int newIndex = _currentIndex + delta;
            _currentIndex = newIndex < 0 ? _size + delta : newIndex % _size;
        }
    }
}
