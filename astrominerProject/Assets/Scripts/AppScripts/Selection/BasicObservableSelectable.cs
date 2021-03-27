using System;
using UnityEngine;

namespace Astrominer
{
    [Serializable]
    public class BasicObservableSelectable : ObservableSelectable
    {
        public bool IsSelected { get; private set; } = false;

        public event Action OnSelection;
        public event Action OnDeselection;

        public void Select()
        {
            if (IsSelected)
                throw new Selectable.AlreadySelectedException();
            IsSelected = true;
            OnSelection?.Invoke();
        }

        public void Deselect()
        {
            if (!IsSelected)
                throw new Selectable.NotSelectedException();
            IsSelected = false;
            OnDeselection?.Invoke();
        }
    }
}