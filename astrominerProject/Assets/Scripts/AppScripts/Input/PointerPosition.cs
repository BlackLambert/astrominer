using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SBaier.Astrominer
{
    public interface PointerPosition
    {
        bool HasPosition { get; }
        Vector2 CurrentPosition { get; }
    }
}
