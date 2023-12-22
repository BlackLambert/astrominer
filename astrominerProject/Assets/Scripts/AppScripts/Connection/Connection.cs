using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class Connection : MonoBehaviour
    {
        [SerializeField] 
        private LineRenderer _renderer;
        
        private Vector3 _start;
        private Vector3 _end;
        
        public void SetEndpoints(Vector3 start, Vector3 end)
        {
            _start = start;
            _end = end;
            UpdateRenderer();
        }

        private void UpdateRenderer()
        {
            _renderer.SetPositions(new Vector3[] {_start, _end});
        }
    }
}
