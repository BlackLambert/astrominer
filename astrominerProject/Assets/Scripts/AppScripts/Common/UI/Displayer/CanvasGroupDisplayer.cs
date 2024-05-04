using System;
using System.Threading.Tasks;
using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class CanvasGroupDisplayer : Displayer
    {
        [SerializeField]
        private CanvasGroup _canvasGroup;

        private async void Awake()
        {
            await Hide(true);
        }

        private void Reset()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
        }

        public override async Task Show(bool immediately)
        {
            await Task.Delay(0);
            _canvasGroup.alpha = 1;
            _canvasGroup.interactable = true;
            _canvasGroup.blocksRaycasts = true;
        }

        public override async Task Hide(bool immediately)
        {
            await Task.Delay(0);
            _canvasGroup.alpha = 0;
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;
        }
    }
}