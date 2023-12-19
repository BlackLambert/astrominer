using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class BasePlacementContext
    {
        public Observable<bool> Started { get; } = new Observable<bool>() { Value = false };
    }
}
