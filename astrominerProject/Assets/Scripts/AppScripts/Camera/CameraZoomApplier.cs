using System;
using System.Collections;
using System.Collections.Generic;
using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class CameraZoomApplier : MonoBehaviour, Injectable
    {
        private Camera _camera;
        private CameraZoom _zoom;
        
        public void Inject(Resolver resolver)
        {
            _camera = resolver.Resolve<Camera>();
            _zoom = resolver.Resolve<CameraZoom>();
        }

        private void OnEnable()
        {
            ApplyNewZoom();
            _zoom.Value.OnValueChanged += OnZoomChanged;
        }

        private void OnDisable()
        {
            _zoom.Value.OnValueChanged -= OnZoomChanged;
        }

        private void OnZoomChanged(float formervalue, float newvalue)
        {
            ApplyNewZoom();
        }

        private void ApplyNewZoom()
        {
            _camera.orthographicSize = _zoom.Value.Value;
        }
    }
}
