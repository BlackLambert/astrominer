using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class PointerFollower2D : MonoBehaviour, Injectable
    {
        [SerializeField] 
        private Transform _followingTransform;

        private PointerPosition _pointerPosition;
        private Camera _camera;
            
        public void Inject(Resolver resolver)
        {
            _pointerPosition = resolver.Resolve<PointerPosition>(0);
            _camera = resolver.Resolve<Camera>();
        }

        private void Update()
        {
            if (_pointerPosition.IsActive)
            {
                _followingTransform.position = (Vector2) _camera.ScreenToWorldPoint(_pointerPosition.CurrentPosition);
            }
        }
    }
}
