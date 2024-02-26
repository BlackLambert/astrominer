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
        [SerializeField]
        private Color _defaultColor = Color.white;
        
        private Vector3 _start;
        private Vector3 _end;
        
        public void SetEndpoints(Vector3 start, Vector3 end)
        {
            _start = start;
            _end = end;
            UpdateRenderer();
        }

        public void SetColor(Color? color)
        {
            if (color.HasValue)
            {
                SetColor(color.Value);
            }
            else
            {
                SetDefaultColor();
            }
        }

        public void SetColor(Color color)
        {
            Color newColor = new Color(color.r, color.g, color.b, _renderer.startColor.a);
            _renderer.startColor = newColor;
            _renderer.endColor = newColor;
        }

        public void SetDefaultColor()
        {
            SetColor(_defaultColor);
        }

        private void UpdateRenderer()
        {
            _renderer.SetPositions(new Vector3[] {_start, _end});
        }
    }
}
