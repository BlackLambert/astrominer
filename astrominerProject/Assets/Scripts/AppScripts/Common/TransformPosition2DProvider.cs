using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class TransformPosition2DProvider : Provider<Vector2>
    {
        private Transform _transform;
        
        public TransformPosition2DProvider(Transform transform)
        {
            _transform = transform;
        }

        public Vector2 Value => _transform.position;
    }
}
