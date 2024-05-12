using System;
using SBaier.DI;
using UnityEngine;
using UnityEngine.UI;

namespace SBaier.Astrominer
{
    public class ClosePopupButton : MonoBehaviour, Injectable, Initializable, Cleanable
    {
        [SerializeField] 
        private Button _button;

        private Popup _popup;
        
        public void Inject(Resolver resolver)
        {
            _popup = resolver.Resolve<Popup>();
        }

        public void Initialize()
        {
            _button.onClick.AddListener(OnClick);
        }

        public void Clean()
        {
            _button.onClick.RemoveListener(OnClick);
        }

        private void OnClick()
        {
            _popup.TriggerOnClose();
        }
    }
}
