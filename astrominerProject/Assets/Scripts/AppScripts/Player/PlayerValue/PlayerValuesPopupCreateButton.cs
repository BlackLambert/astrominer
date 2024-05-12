using SBaier.DI;
using UnityEngine;
using UnityEngine.UI;

namespace SBaier.Astrominer
{
    public class PlayerValuesPopupCreateButton : MonoBehaviour, Injectable, Initializable, Cleanable
    {
        [SerializeField] 
        private Button _button;

        [SerializeField] 
        private Vector2 _popupSize = new Vector2(600, 400);

        [SerializeField] 
        private string _headerText = "Statistics";

        private Pool<Popup, Popup.Arguments> _popupPool;
        private Pool<PlayerValueGraphs, PrefabInstantiationArguments> _graphsPool;
        private Popup _currentPopup;
        private PlayerValueGraphs _currentGraphs;

        public void Inject(Resolver resolver)
        {
            _popupPool = resolver.Resolve<Pool<Popup, Popup.Arguments>>();
            _graphsPool = resolver.Resolve<Pool<PlayerValueGraphs, PrefabInstantiationArguments>>();
        }

        public void Initialize()
        {
            _button.onClick.AddListener(CreatePopup);
        }

        public void Clean()
        {
            _button.onClick.RemoveListener(CreatePopup);
        }

        private void CreatePopup()
        {
            _currentGraphs = _graphsPool.Request(PrefabInstantiationArguments.CreateUIArgs(null));
            _currentPopup = _popupPool.Request(new Popup.Arguments()
            {
                Content = _currentGraphs.RectTransform,
                Size = _popupSize,
                Header = _headerText
            });
            _currentPopup.OnClose += OnPopupClose;
            _currentPopup.transform.SetParent(null);
        }

        private void OnPopupClose()
        {
            _currentPopup.OnClose -= OnPopupClose;
            _currentGraphs.Clean();
            _graphsPool.Return(_currentGraphs);
            _popupPool.Return(_currentPopup);
            _currentPopup = null;
            _currentGraphs = null;
        }
    }
}
