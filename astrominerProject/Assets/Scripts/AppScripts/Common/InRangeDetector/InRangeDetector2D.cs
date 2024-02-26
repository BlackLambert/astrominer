using System;
using System.Collections.Generic;
using UnityEngine;

namespace SBaier.Astrominer
{
    public interface InRangeDetector2D<TItem>
    {
        public event Action<TItem> OnItemCameInRange;
        public event Action<TItem> OnItemCameOutOffRange;
        public IReadOnlyList<TItem> ItemsInRange { get; }
        public Provider<Vector2> StartPoint { get; }
    }
}