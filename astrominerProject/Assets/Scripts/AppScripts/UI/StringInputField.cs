using SBaier.DI;
using UnityEngine;
using TMPro;

namespace SBaier.Astrominer
{
    public class StringInputField : MonoBehaviour, Injectable
    {
        [SerializeField]
        private TMP_InputField _inputField;
        private ActiveItem<string> _chosenString;

        public void Inject(Resolver resolver)
        {
            _chosenString = resolver.Resolve<ActiveItem<string>>();
        }

        private void OnEnable()
        {
            UpdateInputFieldText();
            _inputField.onValueChanged.AddListener(OnValueChanged);
            _chosenString.OnValueChanged += UpdateInputFieldText;
        }

        private void OnDisable()
        {
            _inputField.onValueChanged.RemoveListener(OnValueChanged);
            _chosenString.OnValueChanged -= UpdateInputFieldText;
        }

        private void OnValueChanged(string text)
        {
            _chosenString.Value = text;
        }

        private void UpdateInputFieldText()
        {
            if(_inputField.text != _chosenString.Value)
                _inputField.text = _chosenString.Value;
        }
    }
}
