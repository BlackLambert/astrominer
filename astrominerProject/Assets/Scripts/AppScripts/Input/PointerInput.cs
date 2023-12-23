using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SBaier.Astrominer
{
    public interface PointerInput
    {
        event Action OnDown;
        event Action OnUp;
        event Action OnPress;
        event Action OnClick;
    }
}
