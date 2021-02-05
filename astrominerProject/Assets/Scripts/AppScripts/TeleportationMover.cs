using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Astrominer
{
    public class TeleportationMover : Mover
    {
        private Moveable _moveable;
        public event Action OnTargetReached;

        public void Initialize(Moveable moveable)
        {
            if (moveable is null)
                throw new ArgumentNullException();
            _moveable = moveable;
        }

        public void MoveTo(Vector2 target)
        {
            if (_moveable is null)
                throw new MoveableNullException();
            _moveable.Position = target;
            OnTargetReached?.Invoke();
        }

        public class MoveableNullException: NullReferenceException {}
    }

}


