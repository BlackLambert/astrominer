using System;
using System.Collections.Generic;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class CircularBuffer<T>
    {
        public event Action<T> OnItemPushed;
        public event Action OnChanged;
        
        public int Count { get; private set; } = 0;
        
        private T[] _buffer;
        private int _currentIndex = 0;
        private int _size;
        
        public CircularBuffer(int size)
        {
            _size = size;
            CreateBuffer(size);
        }

        private void CreateBuffer(int size)
        {
            _buffer = new T[size];
        }

        public void Push(T value)
        {
            _buffer[_currentIndex] = value;
            ChangeIndex(1);
            ChangeCount(1);
            OnItemPushed?.Invoke(value);
            OnChanged?.Invoke();
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
            OnChanged?.Invoke();
            return result;
        }

        public void Clear()
        {
            _currentIndex = 0;
            Count = 0;
            ResetBuffer();
            OnChanged?.Invoke();
        }

        private void ResetBuffer()
        {
            for (int i = 0; i < _buffer.Length; i++)
            {
                _buffer[i] = default;
            }
        }

        public IEnumerable<T> GetLastXElementsReverse(int amount)
        {
            ValidateAmount(amount);

            for (int i = 0; i < amount; i++)
            {
                yield return _buffer[GetIndex(_currentIndex - 1 - i)];
            }
        }

        public IEnumerable<T> GetLastXElements(int amount)
        {
            ValidateAmount(amount);
            for (int i = amount - 1; i >= 0; i--)
            {
                yield return _buffer[GetIndex(_currentIndex - 1 - i)];
            }
        }

        private int GetIndex(int index)
        {
            return index < 0 ? _size + index : index % _size;
        }

        private void ValidateAmount(int amount)
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
