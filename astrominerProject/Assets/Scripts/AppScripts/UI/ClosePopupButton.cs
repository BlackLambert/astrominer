using System;
using SBaier.DI;
using UnityEngine;
using UnityEngine.UI;

namespace SBaier.Astrominer
{
    public class ClosePopupButton : MonoBehaviour, Injectable
    {
        [SerializeField] 
        private Button _button;

        private Popup _popup;
        
        public void Inject(Resolver resolver)
        {
            _popup = resolver.Resolve<Popup>();
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(OnClick);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnClick);
        }

        private void OnClick()
        {
            _popup.TriggerOnClose();
        }
    }
}
