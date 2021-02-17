using System;
using UnityEngine;

namespace Astrominer
{
    public abstract class Selectable : MonoBehaviour
    {
        public bool IsSelected { get; private set; } = false;

        public virtual void Select()
        {
            if (IsSelected)
                throw new AlreadySelectedException();
            IsSelected = true;
        }

        public virtual void Deselect()
        {
            if (!IsSelected)
                throw new NotSelectedException();
            IsSelected = false;
        }

        public class NotSelectedException : InvalidOperationException
        {
        }

        public class AlreadySelectedException : InvalidOperationException
        {
        }
    }
}