using SBaier.DI;
using UnityEngine;
using UnityEngine.UI;

namespace SBaier.Astrominer
{
    public class SceneCommandsTriggerButton : CommandsExecutionTriggerBehaviour, Initializable, Cleanable
    {
        [SerializeField]
        private Button _button;

        private Observable<Process.Process> _currentProcess;

        public override void Inject(Resolver resolver)
        {
	        base.Inject(resolver);
	        
	        _currentProcess = resolver.Resolve<Observable<Process.Process>>();
	        _currentProcess.OnValueChanged += OnProcessChanged;
	        UpdateInteractable();
        }

        public void Initialize()
		{
			_button.onClick.AddListener(Execute);
		}

		public void Clean()
		{
			_button.onClick.RemoveListener(Execute);
		}

		private void Reset()
		{
			_button = GetComponent<Button>();
		}

		private void OnProcessChanged(Process.Process formervalue, Process.Process newvalue)
		{
			TryRemoveProcessListeners(formervalue);
			UpdateInteractable();
			TryAddProcessListeners(newvalue);
		}

		private void TryAddProcessListeners(Process.Process process)
		{
			if (process != null)
			{
				process.OnStopped += UpdateInteractable;
				process.OnFinished += UpdateInteractable;
			}
		}

		private void TryRemoveProcessListeners(Process.Process process)
		{
			if (process != null)
			{
				process.OnStopped -= UpdateInteractable;
				process.OnFinished -= UpdateInteractable;
			}
		}

		private void UpdateInteractable()
		{
			_button.interactable = _currentProcess.Value == null || 
			                       _currentProcess.Value.Finished ||
			                       _currentProcess.Value.Stopped;
		}
    }
}
