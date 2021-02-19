using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Astrominer
{
    public class OnClickSelector : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField]
        private Selectable _selectable;
        public Selectable Selectable => _selectable;

        private CurrentSelectionRepository _repository;

        protected virtual void Awake()
        {
            _repository = FindObjectOfType<CurrentSelectionRepository>();
        }

        void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
        {
            _repository.Select(_selectable);
        }
    }
}