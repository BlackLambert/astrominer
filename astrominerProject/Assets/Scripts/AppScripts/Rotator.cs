using System;
using UnityEngine;

namespace Astrominer
{
    public class Rotator: MonoBehaviour
    {
        public Vector2 Facing
        {
            get => transform.up;
        }

        public void Face(Vector2 target)
        {
            transform.up = target - (Vector2)transform.position;
        }
    }
}