using SBaier.DI;
using UnityEngine;
using UnityEngine.UI;

namespace SBaier.Astrominer
{
    public class QuitButton : MonoBehaviour, Injectable, Initializable, Cleanable
    {
        [SerializeField] 
        private Button _button;
        
        private AppQuitter _quitter;
        
        public void Inject(Resolver resolver)
        {
            _quitter = resolver.Resolve<AppQuitter>();
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
            _quitter.Quit();
        }
    }
}
