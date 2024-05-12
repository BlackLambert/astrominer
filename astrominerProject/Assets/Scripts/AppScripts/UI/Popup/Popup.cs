using System;
using SBaier.DI;
using TMPro;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class Popup : MonoBehaviour, Injectable, Initializable
    {
        [SerializeField] 
        private RectTransform _contentHook;

        [SerializeField] 
        private RectTransform _popupTransform;

        [SerializeField]
        private TextMeshProUGUI _header;
        
        public event Action OnClose;

        private Arguments _arguments;
        
        public void Inject(Resolver resolver)
        {
            _arguments = resolver.Resolve<Arguments>();
        }

        public void Initialize()
        {
            _popupTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, _arguments.Size.x);
            _popupTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, _arguments.Size.y);
            _arguments.Content.SetParent(_contentHook, false);
            _arguments.Content.sizeDelta = Vector2.one;
            _arguments.Content.anchoredPosition = Vector2.zero;
            _header.text = _arguments.Header;
        }

        public void TriggerOnClose()
        {
            OnClose?.Invoke();
        }

        public class Arguments
        {
            public RectTransform Content { get; set; }
            public Vector2 Size { get; set; }
            public string Header { get; set; }
        }
    }
}
