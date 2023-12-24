using SBaier.DI;
using UnityEngine;
using UnityEngine.UI;

namespace SBaier.Astrominer
{
    public class SceneCommandsExecutorButton : SceneCommandsExecutor, Injectable
    {
        [SerializeField]
        private Button _button;

		private void OnEnable()
		{
			_button.onClick.AddListener(Execute);
		}

		private void OnDisable()
		{
			_button.onClick.RemoveListener(Execute);
		}
    }
}
