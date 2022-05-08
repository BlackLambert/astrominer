using UnityEngine;
using UnityEngine.UI;

namespace SBaier.Astrominer
{
    public class SceneCommandsExecuterButton : SceneCommandsExecuter
    {
        [SerializeField]
        private Button _button;

		private void OnEnable()
		{
			_button.onClick.AddListener(StartExecuting);
		}

		private void OnDisable()
		{
			_button.onClick.RemoveListener(StartExecuting);
		}

		private void StartExecuting()
		{
			StartCoroutine(ExecuteCommands());
		}
	}
}
