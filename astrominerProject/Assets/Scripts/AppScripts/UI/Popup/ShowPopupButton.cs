using SBaier.DI;
using UnityEngine;
using UnityEngine.UI;

namespace SBaier.Astrominer
{
    public class ShowPopupButton<TPopupContent> : MonoBehaviour, Injectable, Initializable, Cleanable
        where TPopupContent : MonoBehaviour, PopupContent
    {
        [SerializeField] protected Button _button;

        [SerializeField] private Vector2 _popupSize = new Vector2(600, 400);

        [SerializeField] private string _headerText = "Statistics";

        private Pool<Popup, Popup.Arguments> _popupPool;
        private Pool<TPopupContent, PrefabInstantiationArguments> _contentPool;
        private Popup _currentPopup;
        private TPopupContent _currentContent;

        public virtual void Inject(Resolver resolver)
        {
            _popupPool = resolver.Resolve<Pool<Popup, Popup.Arguments>>();
            _contentPool = resolver.Resolve<Pool<TPopupContent, PrefabInstantiationArguments>>();
        }

        public virtual void Initialize()
        {
            _button.onClick.AddListener(CreatePopup);
        }

        public virtual void Clean()
        {
            _button.onClick.RemoveListener(CreatePopup);
        }

        private void CreatePopup()
        {
            _currentContent = _contentPool.Request(PrefabInstantiationArguments.CreateUIArgs(null));
            _currentPopup = _popupPool.Request(new Popup.Arguments()
            {
                Content = _currentContent.RectTransform,
                Size = _popupSize,
                Header = _headerText
            });
            _currentPopup.OnClose += OnPopupClose;
            _currentPopup.transform.SetParent(null);
        }

        private void OnPopupClose()
        {
            _currentPopup.OnClose -= OnPopupClose;
            _contentPool.Return(_currentContent);
            _popupPool.Return(_currentPopup);
            _currentPopup = null;
            _currentContent = null;
        }
    }
}