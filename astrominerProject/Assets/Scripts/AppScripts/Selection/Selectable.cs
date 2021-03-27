using System;
using UnityEngine;

namespace Astrominer
{
    public interface Selectable
    {
        bool IsSelected { get; }

        void Select();

        void Deselect();

        public class NotSelectedException : InvalidOperationException
        {
        }

        public class AlreadySelectedException : InvalidOperationException
        {
        }
    }
}