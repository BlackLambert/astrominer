using System;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class Selectable : MonoBehaviour
    {
        public event Action OnSelected;
        public event Action OnDeselected;
        public bool IsSelected { get; private set; } = false;

        public void Select()
        {
            IsSelected = true;
            OnSelected?.Invoke();
        }

        public void Deselect()
        {
            IsSelected = false;
            OnDeselected?.Invoke();
        }
    }
}
