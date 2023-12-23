using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class MouseInputUpdater : MonoBehaviour, Injectable
    {
        private Arguments _arguments;
        private MouseInput _input;

        private float _clickDuration;
        private float _currentClickDelta;
        private bool _pointerDown;
        private Vector2 _clickStart;
        
        public void Inject(Resolver resolver)
        {
            _arguments = resolver.Resolve<Arguments>();
            _input = resolver.Resolve<MouseInput>(_arguments.Id);
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(_arguments.Id))
            {
                _pointerDown = true;
                _clickStart = Input.mousePosition;
                _input.InvokeOnDown();
            }
            
            if (_pointerDown)
            {
                if (Input.GetMouseButtonUp(_arguments.Id))
                {
                    CheckClick();
                    ClearClickValues();
                    _input.InvokeOnUp();
                    _pointerDown = false;
                }
                else
                {
                    UpdateClickValues();
                    _input.InvokeOnPress();
                }
            }
        }

        private void ClearClickValues()
        {
            _clickDuration = 0;
            _currentClickDelta = 0;
        }

        private void UpdateClickValues()
        {
            _clickDuration += Time.deltaTime;
            _currentClickDelta = (_clickStart - (Vector2)Input.mousePosition).magnitude;
        }

        private void CheckClick()
        {
            if (_clickDuration > _arguments.MaxClickDuration ||
                _currentClickDelta > _arguments.ClickMovementThreshold)
            {
                return;
            }
            
            _input.InvokeOnClick();
        }

        public class Arguments
        {
            public int Id;
            public float MaxClickDuration = 0.3f;
            public float ClickMovementThreshold = 10;
        }
    }
}
