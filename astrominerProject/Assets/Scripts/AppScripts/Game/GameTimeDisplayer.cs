using System;
using SBaier.DI;
using TMPro;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class GameTimeDisplayer : MonoBehaviour, Injectable
    {
        [SerializeField] 
        private TextMeshProUGUI _text;
        [SerializeField] 
        private string _textBase = "Game Time: {0:mm\\:ss}";

        private GameTime _gameTime;
        
        public void Inject(Resolver resolver)
        {
            _gameTime = resolver.Resolve<GameTime>();
        }
        
        private void OnEnable()
        {
            UpdateText();
            _gameTime.Value.OnValueChanged += OnTimeChanged;
        }

        private void OnDisable()
        {
            _gameTime.Value.OnValueChanged -= OnTimeChanged;
        }

        private void UpdateText()
        {
            _text.text = string.Format(_textBase, TimeSpan.FromSeconds(_gameTime.Value.Value));
        }

        private void OnTimeChanged(float formervalue, float newvalue)
        {
            UpdateText();
        }
    }
}
