using UnityEngine;
using System.Collections;
using Zenject;
using UnityEngine.UI;

namespace Astrominer
{
    public class FlyToButton : MonoBehaviour
    {
        [SerializeField]
        private Button _button;
        private CurrentSelectionRepository _currentSelectionRepository;

        [Inject]
        public void Construct(CurrentSelectionRepository repository)
        {
            _currentSelectionRepository = repository;
        }

        private void Start()
        {
            _button.interactable = !_currentSelectionRepository.IsEmpty;
        }
    }
}