using System;
using System.Collections.Generic;

namespace SBaier.Astrominer
{
    public class ObservableList<T>
    {
        protected List<T> _items = new List<T>();
        public event Action<T> OnItemRemoved;
        public event Action<T> OnItemAdded;

		public int Count => Count;

        public void Add(T item)
		{
			ValidateAdd(item);
			_items.Add(item);
			OnItemAdded?.Invoke(item);
		}

		public void Remove(T item)
		{
			ValidateRemove(item);
			_items.Remove(item);
			OnItemRemoved?.Invoke(item);
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
    }
}
