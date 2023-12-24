using System;
using System.Collections.Generic;
using System.Linq;

namespace SBaier.Astrominer
{
    public class ObservableDictionary<K, T> : ObservableList<KeyValuePair<K, T>>, IDictionary<K, T>
    {
		public T this[K key] 
		{ 
			get => Get(key);
			set => Set(key, value);
		}

		public ICollection<K> Keys => _items.Select(i => i.Key).ToList();

		public ICollection<T> Values => _items.Select(i => i.Value).ToList();

		public void Add(K key, T value)
		{
			if (ContainsKey(key))
				throw new InvalidOperationException();
			base.Add(new KeyValuePair<K, T>(key, value));
		}

		public bool ContainsKey(K key)
		{
			return _items.Any(i => i.Key.Equals(key));
		}

		public bool TryGetValue(K key, out T value)
		{
			value = default;
			if (!ContainsKey(key))
				return false;
			value = Get(key);
			return true;
		}

		public T Get(K key)
		{
            KeyValuePair<K, T> result = GetPair(key);
            if (result.Equals(default))
                throw new InvalidOperationException();
            return result.Value;
        }

		public bool Remove(K key)
		{
			if (!ContainsKey(key))
				return false;
			base.Remove(GetPair(key));
			return true;
		}

		public void Set(K key, T value)
		{
			if (!ContainsKey(key))
				Add(key, value);
			else
				Replace(key, value);
		}

		private void Replace(K key, T value)
		{
			int index = _items.FindIndex(i => i.Key.Equals(key));
			_items[index] = new KeyValuePair<K, T>(key, value);
		}

		private KeyValuePair<K, T> GetPair(K key)
		{
			return _items.FirstOrDefault(i => i.Key.Equals(key));
		}
	}
}
