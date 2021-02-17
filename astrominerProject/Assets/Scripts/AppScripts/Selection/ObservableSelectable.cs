using System;
using UnityEngine;

namespace Astrominer
{
    public class ObservableSelectable : Selectable
    {

        public event Action OnSelection;
        public event Action OnDeselection;

        public override void Select()
        {
            base.Select();
            OnSelection?.Invoke();
        }

        public override void Deselect()
        {
            base.Deselect();
            OnDeselection?.Invoke();
        }
    }
}