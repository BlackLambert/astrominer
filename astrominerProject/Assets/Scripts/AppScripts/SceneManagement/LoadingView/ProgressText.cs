using System;
using SBaier.DI;
using TMPro;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class ProgressText : MonoBehaviour, Injectable
    {
        [SerializeField]
        private TextMeshProUGUI _text;
        private SceneChangeProcess _process;
        
        public void Inject(Resolver resolver)
        {
            _process = resolver.Resolve<SceneChangeProcess>();
        }

        private void Start()
        {
            UpdateText();
        }

        private void Update()
        {
            UpdateText();
        }

        private void Reset()
        {
            _text = GetComponent<TextMeshProUGUI>();
        }

        private void UpdateText()
        {
            int percentage = (int)(_process.Progress * 100);
            _text.text = $"{percentage}%";
        }
    }
}