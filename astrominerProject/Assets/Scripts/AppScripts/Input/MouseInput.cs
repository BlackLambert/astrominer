using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class MouseInput : PointerInput
    {
        public event Action OnDown;
        public event Action OnUp;
        public event Action OnPress;
        public event Action OnClick;

        public void InvokeOnDown()
        {
            OnDown?.Invoke();
        }
        
        public void InvokeOnUp()
        {
            OnUp?.Invoke();
        }
        
        public void InvokeOnPress()
        {
            OnPress?.Invoke();
        }
        
        public void InvokeOnClick()
        {
            OnClick?.Invoke();
        }
    }
}
