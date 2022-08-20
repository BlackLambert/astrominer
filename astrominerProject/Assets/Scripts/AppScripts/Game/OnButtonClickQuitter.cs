using UnityEngine;

namespace SBaier.Astrominer
{
    public class OnButtonClickQuitter : MonoBehaviour
    {
        [SerializeField]
        private KeyCode _button = KeyCode.Escape;

		public void Update()
		{
			if (Input.anyKeyDown && Input.GetKeyDown(_button))
				Application.Quit();
		}
	}
}
