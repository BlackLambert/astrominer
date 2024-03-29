using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SBaier.Astrominer
{
    public interface Provider<TItem>
    {
        public TItem Value { get; }
    }
}
