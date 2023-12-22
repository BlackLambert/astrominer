using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class PointerFollower : MonoBehaviour, Injectable
    {
        [SerializeField] 
        private Transform _followingTransform;

        private PointerPosition _pointerPosition;
        private Camera _camera;
            
        public void Inject(Resolver resolver)
        {
            _pointerPosition = resolver.Resolve<PointerPosition>();
            _camera = resolver.Resolve<Camera>();
        }

        private void Update()
        {
            if (_pointerPosition.HasPosition)
            {
                _followingTransform.position = _camera.ScreenToWorldPoint(_pointerPosition.CurrentPosition);
            }
        }
    }
}
