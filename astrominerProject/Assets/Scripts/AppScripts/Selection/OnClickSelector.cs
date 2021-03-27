using System;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Linq;

namespace Astrominer
{
    [RequireComponent(typeof(CurrentSelectionRepository))]
    public class OnClickSelector : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField]
        private Selectable _selectable;

        private CurrentSelectionRepository _repository;

        protected virtual void Awake()
        {
            _repository = SceneObjectFinder.FindSingleInstance<CurrentSelectionRepository>();
        }

        void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
        {
            if (!_repository.IsSelected(_selectable))
                _repository.Select(_selectable);
        }
    }
}