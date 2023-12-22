using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class BasicProvider<TItem> : Provider<TItem>
    {
        public BasicProvider()
        {
            Value = new Observable<TItem>();
        }

        public BasicProvider(TItem item)
        {
            Value = new Observable<TItem>() { Value = item };
        }

        public Observable<TItem> Value { get; }
    }
}
