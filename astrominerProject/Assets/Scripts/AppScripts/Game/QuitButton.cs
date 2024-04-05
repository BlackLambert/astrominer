using SBaier.DI;
using UnityEngine;
using UnityEngine.UI;

namespace SBaier.Astrominer
{
    public class QuitButton : MonoBehaviour, Injectable
    {
        [SerializeField] 
        private Button _button;
        
        private AppQuitter _quitter;
        
        public void Inject(Resolver resolver)
        {
            _quitter = resolver.Resolve<AppQuitter>();
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
            _quitter.Quit();
        }
    }
}
