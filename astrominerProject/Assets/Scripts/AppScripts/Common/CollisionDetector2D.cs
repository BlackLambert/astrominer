using System;
using System.Collections.Generic;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class CollisionDetector2D : MonoBehaviour
    {
        public event Action<Collider2D> OnCollisionEnter;
        public event Action<Collider2D> OnCollisionExit;

        public IReadOnlyList<Collider2D> Collisions => _collisions;
        private List<Collider2D> _collisions = new List<Collider2D>();

        private void OnTriggerEnter2D(Collider2D collider)
        {
            _collisions.Add(collider);
            OnCollisionEnter?.Invoke(collider);
        }

        private void OnTriggerExit2D(Collider2D collider)
        {
            _collisions.Remove(collider);
            OnCollisionExit?.Invoke(collider);
        }
    }
}
