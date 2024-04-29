using UnityEngine;
using UnityEngine.UI;

namespace SBaier.Astrominer
{
    public class SceneCommandsTriggerButton : CommandsExecutionTriggerBehaviour
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

		private void Reset()
		{
			_button = GetComponent<Button>();
		}
    }
}
