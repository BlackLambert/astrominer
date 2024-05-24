using SBaier.DI;
using UnityEngine;
using UnityEngine.UI;

namespace SBaier.Astrominer
{
    public class ProcessProgressBar : MonoBehaviour, Injectable
    {
        [SerializeField] 
        private Image _fillBar;

        private Process.Process _process;

        public void Inject(Resolver resolver)
        {
            _process = resolver.Resolve<Process.Process>();
        }

        private void Update()
        {
            _fillBar.rectTransform.anchorMin = new Vector2(0, 0);
            _fillBar.rectTransform.anchorMax = new Vector2(_process.Progress, 1);
            _fillBar.rectTransform.anchoredPosition = Vector2.zero;
        }

        private void Reset()
        {
            _fillBar = GetComponent<Image>();
        }
    }
}