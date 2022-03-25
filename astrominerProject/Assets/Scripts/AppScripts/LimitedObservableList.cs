using System;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class LimitedObservableList<TItem> : ObservableList<TItem>
    {
		public int Limit { get; private set; }
		public bool LimitReached => _items.Count >= Limit;
		public event Action OnLimitChanged;

		public LimitedObservableList(int limit)
		{
			SetLimit(limit);
		}

		public override void Add(TItem item)
		{
			if (LimitReached)
				throw new LimitReachedException();
			base.Add(item);
		}

		public void SetLimit(int limit)
		{
			if (limit < 0)
				throw new ArgumentOutOfRangeException();
			if (limit < _items.Count)
				throw new InvalidOperationException($"There are more items ({_items.Count}) then the new limit ({limit}). " +
					$"Please remove Items till the new limit is reached.");
			Limit = limit;
			OnLimitChanged?.Invoke();
		}

		public class LimitReachedException : InvalidOperationException { }
	}
}
