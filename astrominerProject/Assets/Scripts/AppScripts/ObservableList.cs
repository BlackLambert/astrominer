using System;
using System.Collections;
using System.Collections.Generic;

namespace SBaier.Astrominer
{
    public class ObservableList<T> : IList<T>
    {
        protected List<T> _items = new List<T>();
        public event Action<T, int> OnItemRemovedAt;
        public event Action<T> OnItemRemoved;
        public event Action<T, int> OnItemAddedAt;
        public event Action<T> OnItemAdded;
        public event ItemReplacedAtAction OnItemReplacedAt;
        public event ItemReplacedAction OnItemReplaced;
        public event Action OnItemsChanged;

		public delegate void ItemReplacedAction(T formerItem, T newItem);
		public delegate void ItemReplacedAtAction(T formerItem, T newItem, int index);

		public int Count => _items.Count;

		public bool IsReadOnly => false;

		public virtual void Add(T item)
		{
			ValidateAdd(item);
			_items.Add(item);
			OnItemAdded?.Invoke(item);
			OnItemAddedAt?.Invoke(item, _items.Count - 1);
			OnItemsChanged?.Invoke();
		}

		public virtual void Remove(T item)
		{
			ValidateRemove(item);
			int index = _items.IndexOf(item);
			Remove(item, index);
		}

		public bool Contains(T item)
		{
			return _items.Contains(item);
		}

		private void ValidateRemove(T item)
		{
			if (!Contains(item))
				throw new ArgumentException();
		}

		private void ValidateAdd(T item)
        {
            if (Contains(item))
                throw new ArgumentException();
        }

		public IReadOnlyList<T> ToReadonly()
		{
			return _items.AsReadOnly();
		}

		public T this[int index]
		{
			get => _items[index];
			set => SetAt(index, value);
		}

		private void SetAt(int index, T value)
		{
			T formerItem = _items[index];
			_items[index] = value;
			OnItemReplaced?.Invoke(formerItem, value);
			OnItemReplacedAt?.Invoke(formerItem, value, index);
			OnItemsChanged?.Invoke();
		}

		public int IndexOf(T item)
		{
			return _items.IndexOf(item);
		}

		public void Insert(int index, T item)
		{
			throw new NotImplementedException();
		}

		public void RemoveAt(int index)
		{
			Remove(_items[index], index);
		}

		private void Remove(T item, int index)
		{
			_items.Remove(item);
			OnItemRemoved?.Invoke(item);
			OnItemRemovedAt?.Invoke(item, index);
			OnItemsChanged?.Invoke();
		}

		public void Clear()
		{
			for (int i = _items.Count - 1; i >= 0; i--)
				RemoveAt(i);
		}

		public void CopyTo(T[] array, int arrayIndex)
		{
			_items.CopyTo(array, arrayIndex);
		}

		bool ICollection<T>.Remove(T item)
		{
			if (!_items.Contains(item))
				return false;
			Remove(item);
			return true;
		}

		public IEnumerator<T> GetEnumerator()
		{
			return _items.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return _items.GetEnumerator();
		}
	}
}
