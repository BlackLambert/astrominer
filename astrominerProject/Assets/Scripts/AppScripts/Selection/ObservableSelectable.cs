using System;
using UnityEngine;

namespace Astrominer
{
    public interface ObservableSelectable: Selectable
    {
        event Action OnSelection;
        event Action OnDeselection;

    }
}
