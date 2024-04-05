using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class OnButtonClickQuitter : MonoBehaviour, Injectable
    {
        [SerializeField]
        private KeyCode _button = KeyCode.Escape;

        private AppQuitter _quitter;

        public void Inject(Resolver resolver)
        {
	        _quitter = resolver.Resolve<AppQuitter>();
        }
        
		public void Update()
		{
			if (Input.anyKeyDown && Input.GetKeyDown(_button))
				_quitter.Quit();
		}
    }
}
