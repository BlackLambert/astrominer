using UnityEngine;

namespace SBaier.Astrominer
{
    public class GamePausedPanelDisplayer : MonoBehaviour
    {
        [SerializeField]
        private GameObject _panel;

        private void Update()
        {
            CheckShowPanel();
        }

        private void CheckShowPanel()
        {
            bool setActive = Time.timeScale == 0;
            if (_panel.activeSelf != setActive)
                _panel.SetActive(setActive);
        }
    }
}
