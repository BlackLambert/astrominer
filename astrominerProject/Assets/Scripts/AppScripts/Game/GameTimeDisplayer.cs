using System;
using SBaier.DI;
using TMPro;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class GameTimeDisplayer : MonoBehaviour, Injectable, Initializable, Cleanable
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
        
        public void Initialize()
        {
            UpdateText();
            _gameTime.Value.OnValueChanged += OnTimeChanged;
        }

        public void Clean()
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
